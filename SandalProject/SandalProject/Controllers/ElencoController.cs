using Microsoft.AspNetCore.Mvc;

namespace SandalProject.Controllers
{
    public class ElencoController : Controller
    {
        public IActionResult Primavera()
        {
            return View();
        }

        public IActionResult Estate()
        {
            return View();
        }

        public IActionResult Autunno()
        {
            return View();
        }

        public IActionResult Inverno()
        {
            return View();
        }
    }
}
