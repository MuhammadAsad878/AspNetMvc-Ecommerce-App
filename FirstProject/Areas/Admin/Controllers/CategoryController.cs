using Ecom.DataAccess.Repository;
using Ecom.DataAccess.Repository.IRepository;
using Ecom.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EcommerceMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller 
    {

        //private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductController> _logger;
        public CategoryController(IUnitOfWork unitOfWork, ILogger<ProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: /Category/Index
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().OrderBy(u=>u.Name).ToList();
            return View(categories);
        }

        // GET: /Category/Upsert/id Update - Insert 
        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                // Create a new category                
                return View();
            }
            Category category = _unitOfWork.Category.Get(u => u.Id == id);

            if (category == null || category.Id > 100)
            {
                return NotFound();
            }

            return View(category);

        }      

        // POST: /Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF protection
        public IActionResult Upsert(Category category)
        {           
            if (ModelState.IsValid)
            {
                if(category.Id > 0)
                {
                    // id greater than 0 update
                    _unitOfWork.Category.Update(category);
                    TempData["SuccessMessage"] = "Category Updated successfully!";
                }
                else
                {
                    // id less than 0 Add
                    _unitOfWork.Category.Add(category);
                    TempData["SuccessMessage"] = "Category Added successfully!";

                }
                // Save changes to database
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            // If validation fails, return to the Add view
            return View("Upsert");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Invalid Category!";
                return RedirectToAction("Index");
            }

            var category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found!";
                return RedirectToAction("Index");
            }

            return View(category);
        }


        // Get: /Category/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Invalid Category!";
                return RedirectToAction("Index");
            }

            var category = _unitOfWork.Category.Get(u => u.Id == id);

            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found!";
                return RedirectToAction("Index");
            }

            // 🔍 Check if any products exist for this category
            Product productsInCategory = _unitOfWork.Product.Get(u=>u.CategoryId == id);

            if (productsInCategory != null)
            {
                TempData["ErrorMessage"] = "Cannot delete category — it's being used by one or more products.";
                return RedirectToAction("Index");
            }

            try
            {
                _unitOfWork.Category.Remove(category);
                _unitOfWork.Save();
                TempData["SuccessMessage"] = "Category removed successfully!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with ID {CategoryId}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the category.";
            }

            return RedirectToAction("Index");
        }


        
        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(Category obj)
        {
            if(obj == null )
            {
                TempData["ErrorMessage"] = "Category Not Found";
                return View();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["SuccessMessage"] = $"Category {obj.Name} Updated successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Ensure that range 1 - 100 for display order";
            }
            
            return RedirectToAction("Index", "Category");
        }
    }
}
