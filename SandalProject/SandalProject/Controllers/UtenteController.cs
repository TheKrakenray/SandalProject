using Microsoft.AspNetCore.Mvc;

namespace SandalProject.Controllers
{
    public class UtenteController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrazione()
        {
            return View();
        }

        public IActionResult Account()
        {
            return View();
        }
    }
}
