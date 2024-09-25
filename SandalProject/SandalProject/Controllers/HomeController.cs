using Microsoft.AspNetCore.Mvc;

namespace SandalProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
