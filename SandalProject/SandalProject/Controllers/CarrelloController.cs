using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;

namespace SandalProject.Controllers
{
    public class CarrelloController : Controller
    {
        public static List<Sandali> carrello = new(); 

        public IActionResult Carrello()
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            Console.WriteLine("id utente " + utenteLoggato.Id);
            if (utenteLoggato.Id != 1)
            {   
                return View(carrello);
            }
            else
            {
                return Redirect("/Utente/Login/");
            }
        }

        public IActionResult InserisciCarrello(int id)
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            Console.WriteLine("Carrello Id " + id);
            Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

            if(s != null)
            {
                carrello.Add(s);

                if(utenteLoggato.Id != 1)
                    DAOAccount.GetInstance().AddCarrello(utenteLoggato,s);
                return Redirect($"/Carrello/Carrello");
            }
            else
            {
                return Redirect($"/Info/NotFound");
            }
        }
        
        public IActionResult EliminaCarrello(int id)
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

            if (s != null)
            {
                carrello.Remove(s);
              
                if(utenteLoggato != null)
                    DAOAccount.GetInstance().RemoveCarrello(utenteLoggato,s) ;
                return Redirect($"/Carrello/Carrello/");
            }
            else
            {
                return Redirect($"/Carrello/Carrello/");
            }
        }

        public IActionResult RiempiCarrello()
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            if (utenteLoggato.Id != 1)
            {
                carrello = DAOAccount.GetInstance().GetCarrello(utenteLoggato);

                return Redirect($"/Carrello/Carrello/");
            }
            else
            {
                return Redirect("/Utente/Login/");
            }
        }

        public IActionResult SvuotaCarrello()
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            if (utenteLoggato.Id != 1)
                DAOAccount.GetInstance().ResetCarrello(utenteLoggato);

            carrello.Clear();
            return Redirect($"/Carrello/Carrello/");
        }
    }
}
