using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;

namespace SandalProject.Controllers
{
    public class WListController : Controller
    {
        public IActionResult WList()
        {
            Account utenteLoggato = UtenteController.utenteLoggato;
            List<Sandali> wList = DAOAccount.GetInstance().GetWList(utenteLoggato);
            if (utenteLoggato.Id > 1)
                return View(wList);
            else
                return Redirect("/Utente/Login/");
        }

        public IActionResult InserisciWList(int id)
        {
            Account utenteLoggato = UtenteController.utenteLoggato;

            Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

            if (utenteLoggato.Id != 1) 
            {
                if (DAOAccount.GetInstance().FindSandaloWList(id))
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
                return Redirect($"/Utente/Login/");
            }
        }

        public IActionResult EliminaWList(int id)
        {
            Account utenteLoggato = UtenteController.utenteLoggato;
            List<Sandali> wList = DAOAccount.GetInstance().GetWList(utenteLoggato);
            Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

            if (s != null)
            {
                wList.Remove(s);

                if (utenteLoggato != null)
                    DAOAccount.GetInstance().RemoveWList(utenteLoggato, s);
                return Redirect($"/WList/WList/{wList}");
            }
            else
            {
                return Redirect($"/WList/WList/{wList}");
            }
        }

        public IActionResult RiempiWList()
        {
            Account utenteLoggato = UtenteController.utenteLoggato;
            List<Sandali> wList = DAOAccount.GetInstance().GetWList(utenteLoggato);
            if (utenteLoggato.Id != 1)
            {
                wList = DAOAccount.GetInstance().GetWList(utenteLoggato);
                return Redirect($"/WList/WList/{wList}");
            }
            return Redirect("/Utente/Login/");
        }

        public IActionResult SvuotaWList()
        {
            Account utenteLoggato = UtenteController.utenteLoggato;
            List<Sandali> wList = DAOAccount.GetInstance().GetWList(utenteLoggato);
            if (utenteLoggato.Id != 1)
                DAOAccount.GetInstance().ResetWList(utenteLoggato);

            wList.Clear();
            return Redirect($"/WList/WList/{wList}");
        }
    }
}
