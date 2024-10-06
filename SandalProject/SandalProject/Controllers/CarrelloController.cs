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

            if (utenteLoggato.Id != 1)
            {
                carrello = DAOAccount.GetInstance().GetCarrello(utenteLoggato);

                return View(carrello);
            }
            else
            {
                return Redirect("/Utente/Logout/");
            }
        }

        public IActionResult InserisciCarrello(int id)
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            if(utenteLoggato.Id != 1)
            {
                Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

                if(s != null)
                {
                    DAOAccount.GetInstance().AddCarrello(utenteLoggato, s);

                    return Redirect($"/Carrello/Carrello");
                }
                else
                {
                    return Redirect($"/Info/NotFound");
                }
            }
            else
            {
                return Redirect($"/Utente/Logout/");
            }
        }

        public IActionResult EliminaCarrello(int id)
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            if (utenteLoggato.Id != 1)
            {
                Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);
                if (s != null)
                {
                    DAOAccount.GetInstance().RemoveCarrello(utenteLoggato, s);

                    return Redirect($"/Carrello/Carrello");
                }
                else
                {
                    return Redirect($"/Info/NotFound");
                }
            }
            else
            {
                return Redirect($"/Utente/Logout/");
            }
        }

        public IActionResult SvuotaCarrello()
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            if (utenteLoggato.Id != 1)
            {
                carrello = DAOAccount.GetInstance().GetCarrello(utenteLoggato);

                DAOAccount.GetInstance().ResetCarrello(utenteLoggato);

                carrello.Clear();

                return Redirect($"/Carrello/Carrello/");
            }
            else
            {
                return Redirect("/Utente/Logout/");
            }
        }

        public IActionResult Paymentone()
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            if (utenteLoggato.Id != 1)
            {
                carrello = DAOAccount.GetInstance().GetCarrello(utenteLoggato);

                if(carrello.Count > 0)
                {
                    return View();
                }
                else
                {
                    return Redirect($"/Carrello/Carrello/");
                }
            }
            else
            {
                return Redirect("/Utente/Logout/");
            }
        }
    }
}
