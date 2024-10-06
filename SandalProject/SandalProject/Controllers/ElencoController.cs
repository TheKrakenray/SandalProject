using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;
using SandalProject.Utility;

namespace SandalProject.Controllers
{
    public class ElencoController : Controller
    {
        List<Sandali> elencoSandali = DAOSandali.GetInstance().ReadAll().Cast<Sandali>().ToList();

        public IActionResult Categoria(string stagione)
        {
            if(stagione.ToLower() == "primavera")
            {
                List<Sandali> primavera = elencoSandali
                                            .Where(a => a.Categoria.Trim('\'').ToLower() == "primavera" || a.Categoria.ToLower() == "spring")
                                            .ToList();
                return View(primavera);
            }

            else if(stagione.ToLower() == "estate")
            {
                List<Sandali> estate = elencoSandali
                                            .Where(a => a.Categoria.Trim('\'').ToLower() == "estate" || a.Categoria.ToLower() == "summer")
                                            .ToList();
                return View(estate);
            }

            else if(stagione.ToLower() == "autunno")
            {
                List<Sandali> autunno = elencoSandali
                                            .Where(a => a.Categoria.Trim('\'').ToLower() == "autunno" || a.Categoria.ToLower() == "autumn" || a.Categoria.ToLower() == "fall")
                                            .ToList();
                return View(autunno);
            }

            else if(stagione.ToLower() == "inverno")
            {
                List<Sandali> inverno = elencoSandali
                                            .Where(a => a.Categoria.Trim('\'').ToLower() == "inverno" || a.Categoria.ToLower() == "winter")
                                            .ToList();
                return View(inverno);
            }

            else
            {
                return Redirect("/Home/Index");
            }
        }

        public IActionResult Risultati(string ricerca/*, Dictionary<string,string>?filtri, List<Sandali>?searchResults*/)
        {

            List<Sandali> risultati = new();

            //if (filtri == null)
            //{
                foreach(var e in elencoSandali)
                {
                    Console.WriteLine(ricerca.Trim('\'').ToLower() + " " + e.Marca.Trim('\'').ToLower());
                    if (ricerca.Trim('\'').ToLower().Contains(e.Marca.Trim('\'').ToLower())) 
                    {
                        risultati.Add(e);
                    }
                }
            Console.WriteLine("------------");
                if(risultati.Count == 0)
                    foreach(var e in elencoSandali)
                    {
                        Console.WriteLine(ricerca.Trim('\'').ToLower() + " " + e.Nome.Trim('\'').ToLower());
                        if (ricerca.Trim('\'').ToLower().Contains(e.Nome.Trim('\'').ToLower()))
                        {
                            risultati.Add(e);
                        }
                    }       
            //}
            //else
            //{
            //    risultati = searchResults;

            //    foreach(var filtro in filtri)
            //    {
            //        switch(filtro.Key.ToLower())
            //        {
            //            case "categoria":
            //                if(filtro.Value.ToLower() == "primavera" || filtro.Value.ToLower() == "spring")
            //                    risultati = risultati
            //                                .Where(a => a.Categoria.ToLower() == "primavera" || a.Categoria.ToLower() == "spring")
            //                                .ToList();

            //                else if(filtro.Value.ToLower() == "estate" || filtro.Value.ToLower() == "summer")
            //                    risultati = risultati
            //                                .Where(a => a.Categoria.ToLower() == "estate" || a.Categoria.ToLower() == "summer")
            //                                .ToList();

            //                else if(filtro.Value.ToLower() == "autunno" || filtro.Value.ToLower() == "autumn" || filtro.Value.ToLower()== "fall")
            //                    risultati = risultati
            //                                .Where(a => a.Categoria.ToLower() == "autunno" || a.Categoria.ToLower() == "fall" || a.Categoria.ToLower() == "autumn")
            //                                .ToList();

            //                else if(filtro.Value.ToLower() == "inverno" || filtro.Value.ToLower() == "winter")
            //                    risultati = risultati
            //                                .Where(a => a.Categoria.ToLower() == "inverno" || a.Categoria.ToLower() == "winter")
            //                                .ToList();
            //                break;
            //            case "prezzomin":
            //                risultati = risultati
            //                .Where(a => a.Prezzo > double.Parse(filtro.Value.Replace(",",".")))
            //                .ToList();
            //                break;
            //            case "prezzomax":
            //                risultati = risultati
            //                .Where(a => a.Prezzo < double.Parse(filtro.Value.Replace(",", ".")))
            //                .ToList();
            //                break;
            //            case "prezzo":
            //                risultati = risultati
            //                .Where(a => a.Prezzo == double.Parse(filtro.Value.Replace(",", ".")))
            //                .ToList();
            //                break;
            //            case "colore":
            //                risultati = risultati
            //                .Where(a => a.Colore == filtro.Value)
            //                .ToList();
            //                break;
            //            case "taglia":
            //                risultati = risultati
            //                .Where(a => a.Taglia == int.Parse(filtro.Value))
            //                .ToList();
            //                break;
            //            case "sconto":
            //                risultati = risultati
            //                .Where(a => a.Sconto == int.Parse(filtro.Value))
            //                .ToList();
            //                break;
            //            case "quantita":
            //                risultati = risultati
            //                .Where(a => a.Quantita == int.Parse(filtro.Value))
            //                .ToList();
            //                break;
            //        }
            //    }
            //}

            return View(risultati);
        }
    }
}
