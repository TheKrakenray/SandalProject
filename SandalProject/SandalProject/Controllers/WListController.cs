using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;

namespace SandalProject.Controllers
{
    public class WListController : Controller
    {
        public IActionResult WList()
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            if (utenteLoggato.Id != 1)
            {
                List<Sandali> wList = DAOAccount.GetInstance().GetWList(utenteLoggato);

                return View(wList);
            }
            else
            {
                return Redirect("/Utente/Logout/");
            }
        }

        public IActionResult InserisciWList(int id)
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            if (utenteLoggato.Id != 1) 
            {
                Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

                if (DAOAccount.GetInstance().FindSandaloWList(utenteLoggato, id))
                {
                    return Redirect($"/Dettagli/Dettagli/{id}"); // Sandalo già presente in wishlist!
                }
                else
                {
                    DAOAccount.GetInstance().AddWList(utenteLoggato, s);

                    return Redirect($"/Dettagli/Dettagli/{id}");
                }
            }
            else
            {
                return Redirect($"/Utente/Logout/");
            }
        }

        public IActionResult EliminaWList(int id)
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            if (utenteLoggato.Id != 1)
            {
                Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

                if (s != null)
                {
                    DAOAccount.GetInstance().RemoveWList(utenteLoggato, s);

                    return Redirect($"/WList/WList/");
                }
                else
                {
                    return Redirect($"/WList/WList/");
                }
            }
            else
            {
                return Redirect($"/Utente/Logout/");
            }
        }

        public IActionResult SvuotaWList()
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            if (utenteLoggato.Id != 1)
            {
                List<Sandali> wList = DAOAccount.GetInstance().GetWList(utenteLoggato);

                DAOAccount.GetInstance().ResetWList(utenteLoggato);

                wList.Clear();

                return Redirect($"/WList/WList/");
            }
            else
            {
                return Redirect("/Utente/Logout/");
            }
        }
    }
}
