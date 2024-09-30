using Microsoft.AspNetCore.Mvc;
using SandalProject.Utility;
using SandalProject.Models;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace SandalProject.Controllers
{
    public class UtenteController : Controller 
    {
        private ILogger<UtenteController> il;
        public static Account utenteLoggato = null;

        public UtenteController(ILogger<UtenteController> l)
        {
            il = l;
        }

        private static int chiamata = 0;
        public IActionResult Login()
        {
            chiamata++;

            il.LogInformation($"Tentativo di accesso numero {chiamata} alle ore {DateTime.Now.Hour}");

            return View(chiamata);
        }

        public IActionResult Valida(Dictionary<string,string> parametri)
        {        
            if (DAOAccount.GetInstance().Valida(parametri["mail"], parametri["psw"]))
            {
                il.LogInformation($"UTENTE LOGGATO: {parametri["mail"]}");

                utenteLoggato = (Account)DAOAccount.GetInstance().Find(parametri["mail"]);

                return Redirect($"/Utente/Account/{utenteLoggato.Id}");
            }
            else
            {
                return Redirect("Login");
            }
        }

        public IActionResult Registrazione()
        {
            return View();
        }

        public IActionResult FormRegistrazione(Dictionary<string,string> parametri)
        {
            if (DAOAccount.GetInstance().Find(parametri["email"]) != null)
            {
                return Content("Errore email già in uso");
            }
            else
            {
                Entity a = SetDefaultPropic();

                parametri.Remove("validapassw");

                a.FromDictionary(parametri);

                if (DAOAccount.GetInstance().Update(a))
                {
                    return Redirect($"/Utente/Account/{a.Id}");
                }
                else
                {
                    return Content("Errore nella creazione dell'account, riprovare!");
                }
            }
        }

        public IActionResult Logout()
        {
            chiamata = 0;
            il.LogInformation($"LOGOUT: {utenteLoggato.Username} alle ore {DateTime.Now.Hour}");
            utenteLoggato = null;

            return Redirect("Login");
        }

        //public IActionResult Account(int id)
        //{
        //    Console.WriteLine(id + " Sono nel UtenteController/Account");
        //    return View(DAOAccount.GetInstance().Find(id));
        //}

        public IActionResult Account() // PROBLEMA -> Se utente loggato, può vedere anche altri account
        {
            if(utenteLoggato == null)
            {
                return Redirect("/Utente/Login"); // Se utente NON loggato
            }
            else
            {
                return View(DAOAccount.GetInstance().Find(utenteLoggato.Id)); // Se utente loggato
            }
        }

        [HttpPost]
        public IActionResult UploadImageToDatabase(IFormFile file, int id)
        {
            Console.WriteLine("Dentro metodo ImgUpload");
            if (file != null && file.Length > 0)
            {
                // Verifica che sia un PNG
                if (file.ContentType == "image/png")
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        var imageBytes = memoryStream.ToArray();
                        Console.WriteLine(id);
                        DAOAccount.GetInstance().ChangeImgDb(id,imageBytes);

                        ViewBag.Message = "Immagine caricata e salvata nel database!";
                        return RedirectToAction("Account",new { id }); // Reindirizza all'account aggiornato
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

        public Entity SetDefaultPropic()
        {
            Entity ricchione = new Account();

            // Trova l'oggetto account da salvare nel DB
            Entity accountDefault = DAOAccount.GetInstance().Find(1);
            var username = ((Account)accountDefault).Username;
            var email = ((Account)accountDefault).Email;
            var psw = ((Account)accountDefault).Psw;
            var ruolo = ((Account)accountDefault).Ruolo;
            var propic = ((Account)accountDefault).Propic;
            var puntiFedelta = ((Account)accountDefault).PFedelta;

            // RICORDARSI I POINTERS
            ((Account)ricchione).Username = username;
            ((Account)ricchione).Email = email;
            ((Account)ricchione).Psw = psw;
            ((Account)ricchione).Ruolo = ruolo;
            ((Account)ricchione).Propic = propic;
            ((Account)ricchione).PFedelta = puntiFedelta;

            // Salva l'account nel DB
            DAOAccount.GetInstance().Insert(ricchione);

            Entity a = DAOAccount.GetInstance().MaxId();

            return a;
        }

        [HttpGet]
        public IActionResult GetImage(int id)
        {
            Console.WriteLine(id + " Sono nel UtenteController/GetImage");
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
