using Microsoft.AspNetCore.Mvc;

namespace SandalProject.Controllers
{
    public class CarrelloController : Controller
    {
        public IActionResult Carrello()
        {
            return View();
        }
    }
}
