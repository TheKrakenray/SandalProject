using SandalProject.Utility;
using System.Text.RegularExpressions;
namespace SandalProject.Models
{
    public class Sandali : Entity
    {
        public Sandali() { }
        public Sandali(int id, string nome, string marca, string descrizione, int prezzo, string sKU, string categoria, string genere, double sconto, int quantita, int taglia, string colore, List<byte[]> immagini): base(id)
        {
            Nome = nome;
            Marca = marca;
            Descrizione = descrizione;
            Prezzo = prezzo;
            SKU = sKU;
            Categoria = categoria;
            Genere = genere;
            Sconto = sconto;
            Quantita = quantita;
            Taglia = taglia;
            Colore = colore;
            Immagini = immagini;
        }

        #region Properties
        private string? _nome;
        public string? Nome  
        {
            get => _nome;
            set
            {
                if (value != null)
                {
                    if (value.Length == 0 || value.Length > 99)
                    {
                        _nome = "default";
                    }
                    else
                    {
                        _nome = value;
                        _nome.Replace("'", "''");
                    }
                }
                else
                    _nome = "null";
            }
        }

        private string? _marca;
        public string? Marca 
        {
            get => _marca;
            set
            {
                if(value != null)
                {
                    if (value.Length == 0 || value.Length > 99)
                    {
                        _marca = "default";
                    }
                    else
                    {
                        _marca = value;
                        _marca.Replace("'", "''");
                    }
                }
                else
                    _marca = "null";
            }
        }
        
        private string? _descrizione;
        public string? Descrizione 
        {
            get => _descrizione;
            set
            {
                if(value != null)
                {
                    if (value.Length == 0 || value.Length > 3999)
                        _descrizione = "default";
                    else
                    {
                        _descrizione = value;
                        _descrizione.Replace("'", "''");
                    }
                }
                else
                    _descrizione = "null";
            }
        }

        private int? _prezzo;
        public int? Prezzo 
        {
            get => _prezzo;
            set
            {
                if(value != null)
                {
                    if (value < 1)
                        _prezzo = 1;
                    else
                        _prezzo = value;
                }
                _prezzo = 1;
               
            } 
        }

        private string? _sku;
        public string? SKU 
        {
            get => _sku;
            set
            {
                if(value != null)
                {
                    Regex skuRGX = new Regex(@"^([A-Z0-9]{8})$");
                    //regex per gli SKU es: A12ZQ23X  può contenere 35^8 valori quindi penso basti
                    if (skuRGX.Match(value).Success)
                        _sku = value;
                    else
                        throw new ArgumentException("Invalid SKU format");
                }
                else _sku = "null";
            }
        }
         
        private string? _categoria;
        public string? Categoria 
        {
            get => _categoria;

            set
            {
                if (value != null)
                {
                    string stagione = "inverno estate primavera autunno winter summer spring autumn fall";
                    if (!stagione.Contains(_categoria))
                    {
                        _categoria = "inverno";
                    }
                    else
                    {
                        _categoria = value;
                        _categoria.Replace("'", "''");
                    }
                }
                else
                    _categoria = "null";
            } 
        }

        private string? _genere;
        public string? Genere
        {
            get => _genere;
            set
            {
                string generi = "uomo donna bambino kid woman man";
                if(value != null)
                {
                    if (!generi.Contains(_genere))
                        _genere = "uomo";
                    else
                    {
                        _genere = value;
                        _genere.Replace("'", "''");
                    }
                }
                else
                    _genere = "null";

            }
        }

        private double? _sconto;
        public double? Sconto 
        {
            get => _sconto;
            set
            {
                if(value != null)
                {
                    if (_sconto < 0)
                    {
                        _sconto = 0;
                    }
                    else if (_sconto > 100)
                    {
                        _sconto = 100;
                    }
                    else
                    {
                        _sconto = value;
                    }
                }
                else
                    _sconto = 0;
            }
        }

        private int? _quantita;
        public int? Quantita 
        { 
            get => _quantita;
            set
            {
                if(value != null)
                {
                    if (value < 0)
                    {
                        throw new ArgumentException("Object should not exist, quantity less than 0");
                    }
                    else
                    {
                        _quantita = value;
                    }
                }
                else
                    _quantita = 1;
            } 
        }

        private int? _taglia;
        public int? Taglia
        {
            get => _taglia;
            set
            {
               if(value != null)
               {
                    if (value < 1)
                    {
                        _taglia = 1;
                    }
                    else
                    {
                        _taglia = value;
                    }
               }
               else 
                    _taglia = 1;
            } 
        }

        private string? _colore;
        public string? Colore
        {
            get => _colore;
            set
            {
                if (value != null)
                {
                    Regex colorRGX = new Regex(@"^#([a-fA-F0-9]{6}|[a-fA-F0-9]{3}|[a-fA-F0-9]{8}|[a-fA-F0-9]{4})$");
                    //regex per i colori (con trasparenza) esadecimali es #ffffffff (bianco senza trasparenza) o #000 (nero)
                    if (colorRGX.Match(value).Success)
                        _colore = value;
                    else
                        _colore = "#ffffff";
                }
                else
                    _colore = "#ffffff";
            }
        }

        public List<byte[]> Immagini { get; set; }
        #endregion
    }
}