using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;
namespace SandalProject.Controllers
{
    public class DettagliController : Controller
    {
        public IActionResult Dettagli(int id)
        {
            Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

            if(s != null)
            {
                return View(s);
            }
            else
            {
                return Redirect("/Info/404");
            }
        }

        [HttpGet]
        public IActionResult GetImage(int id,int posizioneLista)
        {
            Console.WriteLine(id + " Sono nel DettagliController/GetImage");
            List<FileResult> immagini = new List<FileResult>();
            var sandalo = DAOSandali.GetInstance().Find(id);
            var s = (Sandali)sandalo;
            if (s != null && s.Immagine1 != null)
            {
                immagini.Add(File(s.Immagine1, "image/png"));
            }
            if (s != null && s.Immagine2 != null)
            {
                immagini.Add(File(s.Immagine2, "image/png"));
            }
            if (s != null && s.Immagine3 != null)
            {
                immagini.Add(File(s.Immagine3, "image/png"));
            }
            if (s != null && s.Immagine4 != null)
            {
                immagini.Add(File(s.Immagine4, "image/png"));
            }

            if(immagini.Count > 0)
            {
                return immagini[posizioneLista];
            }
            else
            {
                return NotFound();
            }
        }
    }
}
