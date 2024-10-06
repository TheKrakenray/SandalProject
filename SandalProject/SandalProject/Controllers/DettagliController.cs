using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;
namespace SandalProject.Controllers
{
    public class DettagliController : Controller
    {
        public IActionResult Dettagli(int id)
        {
            Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

            string skuSandalo = s.Sku.Substring(0,s.Sku.Length - 4);

            List<Sandali> sandaliList = DAOSandali.GetInstance().CercaColore(skuSandalo, id);

            sandaliList.Insert(0, s);

            if(sandaliList.Count > 0)
            {
                return View(sandaliList);
            }
            else
            {
                return Redirect("/Info/NotFound");
            }
        }

        [HttpGet]
        public IActionResult GetImage(int id,int posizioneLista)
        {
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
