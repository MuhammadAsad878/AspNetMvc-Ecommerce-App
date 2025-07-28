using Ecom.DataAccess.Repository.IRepository;
using Ecom.Models;
using Ecom.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Mono.TextTemplating;

namespace EcommerceMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        // Dependency Injection for UnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, ILogger<ProductController> logger,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        // 6 Routes for Product Controller
        // 1. Index Route
        // 2. Upsert Get  ( For both Update & Insert )
        // 3. Upsert Post ( For both Update & Insert )
        // 4. Delete Route - Post

        // Index Route
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();            
            return View(products);
        }

        // Get - Upsert
        public IActionResult Upsert(int? id)
        {            
            ProductVM productVm = new()
            {
                catListItems = _unitOfWork.Category.GetAll()
                .Select(u => new SelectListItem 
                { 
                    Text = u.Name,
                    Value = u.Id.ToString() 
                }),
                
            };
            if (id == null || id == 0)
            {
                productVm.Product = new Product(); // Create new product
                return View(productVm);
            }
            else
            {
                // Edit existing product
                productVm.Product = _unitOfWork.Product.Get(u => u.Id == id);
                if (productVm.Product == null)
                {
                    TempData["ErrorMessage"] = "Book Not Found!";
                    return RedirectToAction("Index");
                }
                return View(productVm);
            }
        }

        // Post - Upsert 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert([FromForm] ProductVM? productVm, IFormFile? file)
        {
            if(productVm?.Product == null)
            {       
                TempData["ErrorMessage"] = "Invalid Book!";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid Book Data!";
                //return RedirectToAction("Index");
                productVm.catListItems = _unitOfWork.Category.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });
                return View(productVm);
            }

            // Handle file upload if a file is provided
            if(file != null && file.Length > 0)
            {
                // Generate a Unique file name
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                // Path to save the file 
                // wwwroot is the root folder for static files in ASP.NET Core
                string rootPath = _webHostEnvironment.WebRootPath;
                string uploadsFolderPath = Path.Combine(rootPath,@"uploads/products");
                // Ensure the directory exists
                if(!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                // Full path to save the file
                string filePathToSave = Path.Combine(uploadsFolderPath,uniqueFileName);
                // Save the file to the server
                using(FileStream fileStream = new FileStream(filePathToSave, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                if (!String.IsNullOrEmpty(productVm.Product.ImageUrl))
                {
                    // delete existing image also
                    string pathToDelete = Path.Combine(rootPath, productVm.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(pathToDelete)) System.IO.File.Delete(pathToDelete);
                }
                // Set the ImageUrl property of the product to the unique file name
                productVm.Product.ImageUrl = Path.Combine("uploads", "products", uniqueFileName).Replace("\\", "/");
                
            }
            if (productVm.Product.ImageUrl == null)
            {
                productVm.Product.ImageUrl = "";
            }
            try
            {
                if(productVm.Product.Id == 0 )
                {
                    // Create new product
                   _unitOfWork.Product.Add(productVm.Product);                  
                    TempData["SuccessMessage"] = $"{productVm.Product.Title} Added Successfully ";
                }
                else
                {
                    // Update existing product
                    _unitOfWork.Product.Update(productVm.Product);
                    TempData["SuccessMessage"] = $"{productVm.Product.Title} Updated Successfully ";

                }
                _unitOfWork.Save();
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Error Upserting product: {ProductTitle}", productVm.Product.Title);
                TempData["ErrorMessage"] = "An error occurred while UpSerting the book. Please try again."+ex;
                productVm.catListItems = _unitOfWork.Category.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });
                return View(productVm);
            }

            return RedirectToAction("Index");
        }


        

         #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid request. Product ID is missing."
                });
            }

            var product = _unitOfWork.Product.Get(u => u.Id == id);

            if (product == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Product not found."
                });
            }

            try
            {
                // Delete associated image if it exists
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                // Remove product and save changes
                _unitOfWork.Product.Remove(product);
                _unitOfWork.Save();

                return Json(new
                {
                    success = true,
                    message = "Product deleted successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID {ProductId}", id);
                return Json(new
                {
                    success = false,
                    message = "An error occurred while deleting the product. Please try again."
                });
            }
        }


        #endregion
    }
}
