using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;
using SandalProject.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace SandalProject.Controllers
{
    public class HomeController : Controller
    {
        public List<Sandali> sandali = DAOSandali.GetInstance().ReadAll().Cast<Sandali>().ToList();

        public IActionResult Index()
        {
            Dictionary<string, List<Sandali>> parametroView = new();
            //parametroView.Add("SandaloDelMese", SandaloDelMese());
            parametroView.Add("SandaliAll", sandali);

            return View(parametroView);
        }

        public Sandali SandaloDelMese()
        {
            DateTime Data = DateTime.Now;

            string stagione;

            if ((Data.Month == 12 && Data.Day >= 21) || (Data.Month >= 1 && Data.Month < 3) || (Data.Month == 3 && Data.Day < 20))
                stagione = "inverno";

            else if ((Data.Month >= 3 && Data.Day >= 21) || (Data.Month > 3 && Data.Month < 6) || (Data.Month == 6 && Data.Day < 21))
                stagione = "primavera";

            else if ((Data.Month >= 6 && Data.Day >= 21) || (Data.Month > 6 && Data.Month < 9) || (Data.Month == 9 && Data.Day < 22))
                stagione = "estate";
            else
                stagione = "autunno";

            Sandali SandaloDelMese = DAOSandali.GetInstance().SandaloDelMese(stagione);

  

            return SandaloDelMese;
        }
    }
}
