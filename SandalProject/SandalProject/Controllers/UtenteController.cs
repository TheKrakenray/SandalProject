using Microsoft.AspNetCore.Mvc;
using SandalProject.Utility;
using SandalProject.Models;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Reflection.Metadata;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.Drawing;
using System.Text.RegularExpressions;
namespace SandalProject.Controllers
{
    public class UtenteController : Controller
    {
        private ILogger<UtenteController> il;
        public static Account utenteLoggato = (Account)DAOAccount.GetInstance().Find(1);
        private static int chiamata = 0;
        
        public UtenteController(ILogger<UtenteController> l)
        {
            il = l;
            //Rimuovere questo DowloadExcel
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public IActionResult Login()
        {
            chiamata++;

            il.LogInformation($"Tentativo di accesso numero {chiamata} alle ore {DateTime.Now.Hour}:{DateTime.Now.Minute}");

            if (utenteLoggato.Id != 1 && utenteLoggato.Ruolo == "user")
            {
                return Redirect("/Utente/Account/" + utenteLoggato.Id);
            }
            else if (utenteLoggato.Id != 1 && utenteLoggato.Ruolo == "admin")
            {
                return Redirect($"/Utente/Admin/{utenteLoggato.Id}");
            }
            else
            {
                return View(chiamata);
            }
        }

        public IActionResult Valida(Dictionary<string, string> parametri)
        {
            if (DAOAccount.GetInstance().Valida(parametri["mail"], parametri["psw"]))
            {
                il.LogInformation($"UTENTE LOGGATO: {parametri["mail"]} alle ore {DateTime.Now.Hour}:{DateTime.Now.Minute}");

                utenteLoggato = (Account)DAOAccount.GetInstance().Find(parametri["mail"]);

                if (utenteLoggato.Ruolo == "admin")
                {
                    return Redirect($"/Utente/Admin/{utenteLoggato.Id}");
                }
                else
                {
                    return Redirect($"/Utente/Account/{utenteLoggato.Id}");
                }
            }
            else
            {
                return Redirect("/Utente/Login");
            }
        }

        public IActionResult Registrazione()
        {
            return View();
        }

        public IActionResult FormRegistrazione(Dictionary<string, string> parametri)
        {
            if (DAOAccount.GetInstance().Find(parametri["email"]) != null)
            {
                return ViewBag("Errore email già in uso");
            }
            else
            {
                Entity a = SetDefaultPropic();

                parametri.Remove("validapassw");

                a.FromDictionary(parametri);

                if (DAOAccount.GetInstance().Update(a))
                {
                    utenteLoggato = (Account)DAOAccount.GetInstance().Find(a.Id);
                    return Redirect($"/Utente/Account/{a.Id}");
                }
                else
                {
                    return ViewBag("Errore nella creazione dell'account, riprovare!");
                }
            }
        }

        public IActionResult Logout()
        {
            chiamata = 0;
            il.LogInformation($"LOGOUT: {utenteLoggato.Email} alle ore {DateTime.Now.Hour}:{DateTime.Now.Minute}");
            utenteLoggato = (Account)DAOAccount.GetInstance().Find(1);

            return Redirect("/Utente/Login");
        }

        public IActionResult Account(int id)
        {
            if (utenteLoggato == (Account)DAOAccount.GetInstance().Find(1) || id != utenteLoggato.Id || id == 1)
            {
                return Redirect("/Utente/Login"); // Se utente NON loggato
            }
            else
            {
                return View(DAOAccount.GetInstance().Find(id)); // Se utente loggato
            }
        }

        [HttpPost]
        public IActionResult UploadImageToDatabase(IFormFile file, int id)
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
                        DAOAccount.GetInstance().ChangeImgDb(id, imageBytes);

                        ViewBag.Message = "Immagine caricata e salvata nel database!";
                        return RedirectToAction("Account", new { id }); // Reindirizza all'account aggiornato
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
            Entity temp = new Account();

            // Trova l'oggetto account da salvare nel DB
            Entity accountDefault = DAOAccount.GetInstance().Find(1);
            var username = ((Account)accountDefault).Username;
            var email = ((Account)accountDefault).Email;
            var psw = ((Account)accountDefault).Psw;
            var ruolo = ((Account)accountDefault).Ruolo;
            var propic = ((Account)accountDefault).Propic;
            var puntiFedelta = ((Account)accountDefault).PFedelta;

            // RICORDARSI I POINTERS
            ((Account)temp).Username = username;
            ((Account)temp).Email = email;
            ((Account)temp).Psw = psw;
            ((Account)temp).Ruolo = ruolo;
            ((Account)temp).Propic = propic;
            ((Account)temp).PFedelta = puntiFedelta;

            // Salva l'account nel DB
            DAOAccount.GetInstance().Insert(temp);

            Entity a = DAOAccount.GetInstance().MaxId();

            return a;
        }

        [HttpGet]
        public IActionResult GetImage(int id)
        {
            var account = DAOAccount.GetInstance().Find(id);
            var image = (Account)account;
            if (image != null && image.Propic != null)
            {
                return File(image.Propic, "image/png");
            }
            return NotFound();
        }

        public IActionResult Admin(int id)
        {
            if (utenteLoggato.Id != 1 && utenteLoggato.Ruolo == "admin")
            {
                return View(utenteLoggato);
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        public IActionResult CaricaFileSandali(IFormFile filexlsx)
        {
            if (filexlsx == null)
            {
                Console.WriteLine("File non ricevuto.");
                return BadRequest("File non ricevuto.");
            }

            List<Entity> e = new List<Entity>();
            //int skuId = DAOSandali.GetInstance().GetId();
            //string nome = "";
            List<Entity> sandali = DAOSandali.GetInstance().ReadAll();
            //Console.WriteLine(filexlsx.ContentType);

            if (filexlsx != null && filexlsx.Length > 0 && filexlsx.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                // Utilizza EPPlus per caricare il file Excel
                using (var stream = new MemoryStream())
                {
                    // Copia il file in un MemoryStream
                    filexlsx.CopyTo(stream);
                    stream.Position = 0; // Riporta il puntatore all'inizio

                    using (var package = new ExcelPackage(stream))
                    {
                        // Seleziona il primo foglio di lavoro
                        var worksheet = package.Workbook.Worksheets[0];

                        // Inizia dalla riga 2 per saltare l'intestazione
                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            // Estrazione delle immagini dal foglio
                            byte[] img1 = null, img2 = null, img3 = null, img4 = null;

                            foreach (var drawing in worksheet.Drawings)
                            {
                                if (drawing is ExcelPicture picture && picture.From.Row + 1 == row)  // picture.From.Row è 0-indexed
                                {
                                    // Verifica la colonna dell'immagine e assegna correttamente la posizione
                                    switch (picture.From.Column + 1) // picture.From.Column è 0-indexed
                                    {
                                        case 13:
                                            img1 = picture.Image.ImageBytes;
                                            break;
                                        case 14:
                                            img2 = picture.Image.ImageBytes;
                                            break;
                                        case 15:
                                            img3 = picture.Image.ImageBytes;
                                            break;
                                        case 16:
                                            img4 = picture.Image.ImageBytes;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            Entity ent = new Sandali
                            {
                                Nome = worksheet.Cells[row, 1].Text,
                                Marca = worksheet.Cells[row, 2].Text,
                                Descrizione = worksheet.Cells[row, 3].Text + "|" + worksheet.Cells[row, 4].Text + "|" + worksheet.Cells[row, 5].Text,// 4 - 5
                                Prezzo = double.Parse(worksheet.Cells[row, 6].Text),
                                Categoria = worksheet.Cells[row, 7].Text,
                                Genere = worksheet.Cells[row, 8].Text,
                                Sconto = double.Parse(worksheet.Cells[row, 9].Text),
                                Quantita = int.Parse(worksheet.Cells[row, 10].Text),
                                Taglia = int.Parse(worksheet.Cells[row, 11].Text),
                                Immagine1 = img1,
                                Immagine2 = img2,
                                Immagine3 = img3,
                                Immagine4 = img4,
                                Sku = "",
                                Colore = worksheet.Cells[row, 12].Text
                            };

                            string sku = $"{((Sandali)ent).Nome[0]}" + $"{((Sandali)ent).Nome[1]}" + $"{((Sandali)ent).Marca[0]}" + $"{((Sandali)ent).Marca[1]}" + $"{((Sandali)ent).Categoria[0]}" + $"{((Sandali)ent).Genere[0]}";

                            ((Sandali)ent).Sku = sku.ToUpper();

                            e.Add(ent);
                        }
                    }
                }

                DAOSandali.GetInstance().AggiornaTabella();

                foreach (Entity ents in e)
                {
                    DAOSandali.GetInstance().Insert(ents);
                }
            }

            return Redirect($"/Utente/Admin/{utenteLoggato.Id}");
        }

        public IActionResult RecuperaPassword()
        {
            return View();
        }


        //Rimuovere questo DowloadExcel
        [HttpGet]
        public IActionResult DownloadExcel()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells[1, 1].Value = "Header1";
                worksheet.Cells[1, 2].Value = "Header2";
                worksheet.Cells[2, 1].Value = "Data1";
                worksheet.Cells[2, 2].Value = "Data2";

                var stream = new MemoryStream();
                package.SaveAs(stream);
                var fileName = "MyExcelFile.xlsx";

                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
