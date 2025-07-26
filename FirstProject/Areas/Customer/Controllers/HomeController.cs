using System.Diagnostics;
using Ecom.DataAccess.Repository.IRepository;
using Ecom.Models;
using Ecom.Models.ViewModels;
using FirstProject.Controllers;
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
                PageSize = 10,
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
