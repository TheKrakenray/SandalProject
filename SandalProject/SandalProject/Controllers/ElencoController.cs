using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;
using SandalProject.Utility;

namespace SandalProject.Controllers
{
    public class ElencoController : Controller
    {
        List<Sandali> Elenco = DAOSandali.GetInstance().ReadAll().Cast<Sandali>().ToList();

        public IActionResult Primavera()
        {
            List<Sandali> primavera =   Elenco
                                        .Where(a => a.Categoria.ToLower() == "primavera" || a.Categoria.ToLower() == "spring")
                                        .ToList();
            return View(primavera);
        }

        public IActionResult Estate()
        {
            List<Sandali> estate =  Elenco
                                    .Where(a => a.Categoria.ToLower() == "summer" || a.Categoria.ToLower() == "estate")
                                    .ToList();
            return View(estate);
        }

        public IActionResult Autunno()
        {
            List<Sandali> autunno = Elenco
                                    .Where(a => a.Categoria.ToLower() == "autunno" || a.Categoria.ToLower() == "fall" || a.Categoria.ToLower() == "autumn")
                                    .ToList();
            return View(autunno);
        }

        public IActionResult Inverno()
        {
            List<Sandali> inverno = Elenco
                                    .Where(a => a.Categoria.ToLower() == "winter" || a.Categoria.ToLower() == "inverno")
                                    .ToList();
            return View(inverno);
        }

        public IActionResult Risultati(string ricerca, Dictionary<string,string>?filtri, List<Sandali>?searchResults)
        {

            List<Sandali> risultati = new();

            if (filtri == null)
            {
                foreach(var e in Elenco)
                    if (ricerca.ToLower().Contains(e.Marca.ToLower())) 
                    {
                       risultati.Add(e);
                    }
                if(risultati == null)
                    foreach(var e in Elenco)
                    {
                        if (ricerca.ToLower().Contains(e.Nome.ToLower()))
                        {
                            risultati.Add(e);
                        }
                    }       
            }
            else
            {
                risultati = searchResults;

                foreach(var filtro in filtri)
                {
                    switch(filtro.Key.ToLower())
                    {
                        case "categoria":
                            if(filtro.Value.ToLower() == "primavera" || filtro.Value.ToLower() == "spring")
                                risultati = risultati
                                            .Where(a => a.Categoria.ToLower() == "primavera" || a.Categoria.ToLower() == "spring")
                                            .ToList();

                            else if(filtro.Value.ToLower() == "estate" || filtro.Value.ToLower() == "summer")
                                risultati = risultati
                                            .Where(a => a.Categoria.ToLower() == "estate" || a.Categoria.ToLower() == "summer")
                                            .ToList();

                            else if(filtro.Value.ToLower() == "autunno" || filtro.Value.ToLower() == "autumn" || filtro.Value.ToLower()== "fall")
                                risultati = risultati
                                            .Where(a => a.Categoria.ToLower() == "autunno" || a.Categoria.ToLower() == "fall" || a.Categoria.ToLower() == "autumn")
                                            .ToList();

                            else if(filtro.Value.ToLower() == "inverno" || filtro.Value.ToLower() == "winter")
                                risultati = risultati
                                            .Where(a => a.Categoria.ToLower() == "inverno" || a.Categoria.ToLower() == "winter")
                                            .ToList();
                            break;
                        case "prezzomin":
                            risultati = risultati
                            .Where(a => a.Prezzo > double.Parse(filtro.Value.Replace(",",".")))
                            .ToList();
                            break;
                        case "prezzomax":
                            risultati = risultati
                            .Where(a => a.Prezzo < double.Parse(filtro.Value.Replace(",", ".")))
                            .ToList();
                            break;
                        case "prezzo":
                            risultati = risultati
                            .Where(a => a.Prezzo == double.Parse(filtro.Value.Replace(",", ".")))
                            .ToList();
                            break;
                        case "colore":
                            risultati = risultati
                            .Where(a => a.Colore == filtro.Value)
                            .ToList();
                            break;
                        case "taglia":
                            risultati = risultati
                            .Where(a => a.Taglia == int.Parse(filtro.Value))
                            .ToList();
                            break;
                        case "sconto":
                            risultati = risultati
                            .Where(a => a.Sconto == int.Parse(filtro.Value))
                            .ToList();
                            break;
                        case "quantita":
                            risultati = risultati
                            .Where(a => a.Quantita == int.Parse(filtro.Value))
                            .ToList();
                            break;
                    }
                }
            }

            return View(risultati);
        }
    }
}
