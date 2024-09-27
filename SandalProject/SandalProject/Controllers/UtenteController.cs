using Microsoft.AspNetCore.Mvc;
using SandalProject.Utility;
using SandalProject.Models;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

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

        public IActionResult Account(int id)
        {
            Console.WriteLine(id + "Sono nel UtenteController/Account");
            return View(DAOAccount.GetInstance().Find(id));
        }

        [HttpPost]
        public IActionResult UploadImageToDatabase(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Verifica che sia un PNG
                if (file.ContentType == "image/png")
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        var imageBytes = memoryStream.ToArray();

                        // Crea l'oggetto account da salvare nel DB
                        Account account = new Account
                        {
                            Username = "Domenico",
                            Email = "gay@gmail.com",
                            Psw = "frocio",
                            Ruolo = "user",
                            PFedelta = 10,
                            Propic = imageBytes
                        };

                        // Salva l'account nel DB
                        Entity e = (Entity)account;
                        DAOAccount.GetInstance().Insert(e);

                        ViewBag.Message = "Immagine caricata e salvata nel database!";
                        return RedirectToAction("Account", new { id = e.Id }); // Reindirizza all'account creato
                    }
                }
                else
                {
                    ViewBag.Message = "Il file deve essere un'immagine PNG!";
                }
            }
            else
            {
                ViewBag.Message = "Nessun file selezionato!";
            }
            return RedirectToAction("Account");
        }

        [HttpGet]
        public IActionResult GetImage(int id)
        {
            Console.WriteLine(id + "Sono nel UtenteController/GetImage");
            var account = DAOAccount.GetInstance().Find(id);
            var image = (Account)account;
            if (image != null && image.Propic != null)
            {
                return File(image.Propic, "image/png");
            }
            return NotFound();
        }

    }
}
