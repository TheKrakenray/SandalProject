using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;

namespace SandalProject.Controllers
{
    public class CarrelloController : Controller
    {
        static Account utenteLoggato = UtenteController.utenteLoggato;
        public static List<Sandali> carrello = new(); 

        public IActionResult Carrello(List<Sandali> carrello)
        {
            if(utenteLoggato != null)
            {
                return View(carrello);
            }
            else
            {
                return Redirect("/Utente/Login");
            }
        }

        public IActionResult InserisciCarrello(int id)
        {
            Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

            if(s != null)
            {
                carrello.Add(s);

                if(utenteLoggato!= null)
                    DAOAccount.GetInstance().AddCarrello(utenteLoggato,s);
                return Redirect($"/Dettagli/Dettagli/{id}");
            }
            else
            {
                return Redirect($"/Home/Index/");
            }
        }
        
        public IActionResult EliminaCarrello(int id)
        {
            Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

            if (s != null)
            {
                carrello.Remove(s);
              
                if(utenteLoggato!=null)
                    DAOAccount.GetInstance().RemoveCarrello(utenteLoggato,s) ;
                return Redirect($"/Carrello/Carrello/");
            }
            else
            {
                return Redirect($"/Carrello/Carrello/{carrello}");
            }
        }

        public IActionResult RiempiCarrello()
        {
            if(utenteLoggato != null)
            {
                carrello = DAOAccount.GetInstance().GetCarrello(utenteLoggato);

                return Redirect($"/Carrello/Carrello/{carrello}");
            }
            else
            {
                return Redirect("/Utente/Login/");
            }
        }

        public IActionResult SvuotaCarrello()
        {
            if(utenteLoggato != null)
                DAOAccount.GetInstance().ResetCarrello(utenteLoggato);

            carrello.Clear();
            return Redirect($"/Carrello/Carrello/{carrello}");
        }
    }
}
