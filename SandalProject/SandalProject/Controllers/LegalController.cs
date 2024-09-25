using Microsoft.AspNetCore.Mvc;

namespace SandalProject.Controllers
{
    public class LegalController : Controller
    {
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Cookies()
        {
            return View();
        }
    }
}
