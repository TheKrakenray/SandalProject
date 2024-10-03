using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;

namespace SandalProject.Controllers
{
    public class WListController : Controller
    {
        static Account utenteLoggato = UtenteController.utenteLoggato;
        public static List<Sandali> wList = DAOAccount.GetInstance().GetWList(utenteLoggato);

        public IActionResult WList(List<Sandali> wList)
        {
            if (utenteLoggato.Id > 1)
                return View(wList);
            else
                return Redirect("/Utente/Login/");
        }

        public IActionResult InserisciWList(int id)
        {
            Sandali s = (Sandali)DAOSandali.GetInstance().Find(id);

            if (s != null)
            {
                wList.Add(s);

                if (utenteLoggato != null)
                    DAOAccount.GetInstance().AddWList(utenteLoggato, s);
                return Redirect($"/Dettagli/Dettagli/{id}");
            }
            else
            {
                return Redirect($"/Home/Index/");
            }
        }

        public IActionResult EliminaWList(int id)
        {
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
            if (utenteLoggato.Id != 1)
            {
                wList = DAOAccount.GetInstance().GetWList(utenteLoggato);
                return Redirect($"/WList/WList/{wList}");
            }
            return Redirect("/Utente/Login/");
        }

        public IActionResult SvuotaWList()
        {
            if (utenteLoggato.Id != 1)
                DAOAccount.GetInstance().ResetWList(utenteLoggato);

            wList.Clear();
            return Redirect($"/WList/WList/{wList}");
        }
    }
}
