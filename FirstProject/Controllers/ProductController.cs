using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
