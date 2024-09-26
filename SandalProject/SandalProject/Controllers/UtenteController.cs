﻿using Microsoft.AspNetCore.Mvc;
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
            //byte[] image = DAOAccount.GetInstance().GetImg(id); // Usa il tuo metodo per ottenere l'immagine

            //if (image == null || image.Length == 0)
            //{
            //    return NotFound(); // Restituisci 404 se non trovata
            //}

            //FileContentResult img = File(image, "image/png"); // Restituisci l'immagine

            //Entity e = DAOAccount.GetInstance().Find(id);

            //Dictionary<Entity, FileContentResult> dic = new ();

            //dic.Add(e, img);
            Entity e = DAOAccount.GetInstance().Find(id);
            var image = ((Account)e).Propic;

            ViewBag.Image = $"data:image/png;base64,{image}";
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
            return Redirect("Account");
        }


        //public IActionResult GetImage(Entity e)
        //{
        //    Console.WriteLine("Dento metodo GetImage con Entity");
        //    var image = ((Account)e).Propic;

        //    Console.WriteLine(((Account)e).Propic);

        //    if (image != null)
        //    {
        //        return File(image, "image/png");
        //    }

        //    return NotFound();
        //}

        //[HttpGet("GetImage/{id}")]
        //public IActionResult GetImage(int id)
        //{
        //    byte[] image = DAOAccount.GetInstance().GetImg(id); // Usa il tuo metodo per ottenere l'immagine

        //    if (image == null || image.Length == 0)
        //    {
        //        return NotFound(); // Restituisci 404 se non trovata
        //    }

        //    return File(image, "image/png"); // Restituisci l'immagine
        //}
    }
}
