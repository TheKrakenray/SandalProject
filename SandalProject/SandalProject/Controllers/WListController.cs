using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;

namespace SandalProject.Controllers
{
    public class WListController : Controller
    {
        public static List<Sandali> wList = new();

        public IActionResult WList()
        {
            Account utenteLoggato = new(); //UtenteController.UtenteLoggato
            if (utenteLoggato != null)
                return View(wList);
            else
                return Redirect("/Utente/Login");
        }

        public IActionResult InserisciWList(int id)
        {
            Sandali s = new();
            s = (Sandali)DAOSandali.GetInstance().Find(id);
            if (s != null)
            {
                wList.Add(s);
                Account utenteLoggato = new(); //UtenteController.UtenteLoggato
                if (utenteLoggato != null)
                    DAOAccount.GetInstance().AddWList(utenteLoggato, s);
                return Redirect($"Dettagli/Dettagli/{id}");
            }
            else
            {
                return Redirect($"Home/Index/");
            }
        }

        public IActionResult EliminaWList(int id)
        {
            Sandali s = new();
            s = (Sandali)DAOSandali.GetInstance().Find(id);
            if (s != null)
            {
                wList.Remove(s);
                Account utenteLoggato = new(); //UtenteController.UtenteLoggato
                if (utenteLoggato != null)
                    DAOAccount.GetInstance().RemoveWList(utenteLoggato, s);
                return Redirect($"WList/WList/");
            }
            else
            {
                return Redirect($"WList/WList/");
            }
        }

        public IActionResult RiempiWList()
        {
            Account utenteLoggato = new(); //UtenteController.UtenteLoggato
            if (utenteLoggato != null)
            {
                wList = DAOAccount.GetInstance().GetWList(utenteLoggato);
                return Redirect("WList/WList");
            }
            return Redirect("Utente/Login/");
        }

        public IActionResult SvuotaWList()
        {
            Account utenteLoggato = new(); //UtenteController.UtenteLoggato
            if (utenteLoggato != null)
                DAOAccount.GetInstance().ResetWList(utenteLoggato);

            wList.Clear();
            return Redirect("Utente/Account/");
        }
    }
}
