using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;

namespace SandalProject.Controllers
{
    public class CarrelloController : Controller
    {
        public static List<Sandali> carrello = new();
        public IActionResult Carrello()
        {
            return View(carrello);
        }

        public IActionResult InserisciCarrello(int id)
        {
            Sandali s = new();
            s = (Sandali)DAOSandali.GetInstance().Find(id);
            if(s != null)
            {
                carrello.Add(s);
                Account utenteLoggato = new(); //UtenteController.UtenteLoggato
                if(utenteLoggato!= null)
                    DAOAccount.GetInstance().AddCarrello(utenteLoggato,s);
                return Redirect($"Dettagli/Dettagli/{id}");
            }
            else
            {
                return Redirect($"Home/Index/");
            }
        }
        
        public IActionResult EliminaCarrello(int id)
        {
            Sandali s = new();
            s = (Sandali)DAOSandali.GetInstance().Find(id);
            if (s != null)
            {
                carrello.Remove(s);
                Account utenteLoggato = new(); //UtenteController.UtenteLoggato
                if(utenteLoggato!=null)
                    DAOAccount.GetInstance().RemoveCarrello(utenteLoggato,s) ;
                return Redirect($"Carrello/Carrello/");
            }
            else
            {
                return Redirect($"Carrello/Carrello/");
            }
        }

        public IActionResult RiempiCarrello()
        {
            Account utenteLoggato = new(); //UtenteController.UtenteLoggato
            if(utenteLoggato != null)
                carrello = DAOAccount.GetInstance().GetCarrello(utenteLoggato);
            return Redirect("Utente/Login/");
        }

        public IActionResult SvuotaCarrello()
        {
            Account utenteLoggato = new(); //UtenteController.UtenteLoggato
            if(utenteLoggato != null)
                DAOAccount.GetInstance().ResetCarrello(utenteLoggato);

            carrello.Clear();
            return Redirect("Utente/Account/");
        }
    }
}
