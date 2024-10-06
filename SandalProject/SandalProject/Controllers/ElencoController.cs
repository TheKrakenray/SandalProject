using Microsoft.AspNetCore.Mvc;
using SandalProject.Models;
using SandalProject.Utility;

namespace SandalProject.Controllers
{
    public class ElencoController : Controller
    {
        List<Sandali> elencoSandali = DAOSandali.GetInstance().ReadAll().Cast<Sandali>().ToList();

        public IActionResult Categoria(string ricerca)
        {
            if(ricerca.ToLower() == "primavera")
            {
                List<Sandali> primavera = elencoSandali
                                            .Where(a => a.Categoria.Trim('\'').ToLower() == "primavera" || a.Categoria.ToLower() == "spring")
                                            .ToList();
                return View(primavera);
            }

            else if(ricerca.ToLower() == "estate")
            {
                List<Sandali> estate = elencoSandali
                                            .Where(a => a.Categoria.Trim('\'').ToLower() == "estate" || a.Categoria.ToLower() == "summer")
                                            .ToList();
                return View(estate);
            }

            else if(ricerca.ToLower() == "autunno")
            {
                List<Sandali> autunno = elencoSandali
                                            .Where(a => a.Categoria.Trim('\'').ToLower() == "autunno" || a.Categoria.ToLower() == "autumn" || a.Categoria.ToLower() == "fall")
                                            .ToList();
                return View(autunno);
            }

            else if(ricerca.ToLower() == "inverno")
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

        public IActionResult Genere(string ricerca)
        {
            if(ricerca.ToLower() == "uomo")
            {
                List<Sandali> uomo = elencoSandali
                                            .Where(a => a.Genere.Trim('\'').ToLower() == "uomo" || a.Genere.ToLower() == "man")
                                            .ToList();
                return View(uomo);
            }

            else if(ricerca.ToLower() == "donna")
            {
                List<Sandali> donna = elencoSandali
                                            .Where(a => a.Genere.Trim('\'').ToLower() == "donna" || a.Genere.ToLower() == "woman")
                                            .ToList();
                return View(donna);
            }

            else if(ricerca.ToLower() == "bambino")
            {
                List<Sandali> bambino = elencoSandali
                                            .Where(a => a.Genere.Trim('\'').ToLower() == "bambino" || a.Genere.ToLower() == "kid")
                                            .ToList();
                return View(bambino);
            }

            else
            {
                return Redirect("/Home/Index");
            }
        }

        public IActionResult Risultati(string ricerca, Dictionary<string,string>?filtri, List<Sandali>?searchResults)
        {

            List<Sandali> risultati = new();

            if (filtri == null)
            {
                foreach(var e in elencoSandali)
                    if (ricerca.ToLower().Contains(e.Marca.ToLower())) 
                    {
                       risultati.Add(e);
                    }

                if(risultati == null)
                    foreach(var e in elencoSandali)
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
