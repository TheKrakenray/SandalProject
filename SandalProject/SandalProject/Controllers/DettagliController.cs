using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;
namespace SandalProject.Controllers
{
    public class DettagliController : Controller
    {
        public IActionResult Dettagli(Sandali s)
        {
            return View(s);
        }
    }
}
