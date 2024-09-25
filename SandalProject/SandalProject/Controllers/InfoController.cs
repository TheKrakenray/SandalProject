using Microsoft.AspNetCore.Mvc;

namespace SandalProject.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Contatti()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
