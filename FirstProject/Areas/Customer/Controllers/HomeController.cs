using System.Diagnostics;
using Ecom.DataAccess.Repository.IRepository;
using Ecom.Models;
using Ecom.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController( IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public IActionResult Index(int page)
        {
            // Fetch all products from the database using the unit of work pattern
            List<Product> products = _unitOfWork.Product.GetAll().ToList();
            PaginationVM p = new()
            {
                Count = products.Count(),
                PageSize = 12,
                CurrPage = page,
            }; 
            if (page > 0)
            {
                p.products = products.Skip((page - 1) * p.PageSize).Take(p.PageSize);
            }
            else
            {
                p.products = products.Take(p.PageSize);
            }
            // Set the ViewBag properties for pagination
            return View(p);
        }

        public IActionResult Details(int? id)
        {
            if(id == null || id <= 0)
            {
                return NotFound();
            }
            // Fetch the product details from the database using the unit of work pattern
            Product product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "Category");
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ActionName("wishlist")]
        public IActionResult WishList()
        {
            return View();
        }

        [ActionName("wishlist2")]
        public IActionResult WishList2()
        {
            return View();
        }
        [ActionName("check-out")]
        public IActionResult CheckOut()
        {
            return View();
        }




        #region

        // This is a private method that can be used for internal logic within the controller

        #endregion
    }
}
