using SandalProject.Utility;
using System.Text.RegularExpressions;
namespace SandalProject.Models
{
    public class Sandali : Entity
    {
        public Sandali() { }
        public Sandali(int id, string nome, string marca, string descrizione, double? prezzo, string sku, string categoria, string genere, double? sconto, int? quantita, int? taglia, byte[] immagine1, byte[] immagine2, byte[] immagine3, byte[] immagine4, string colore) : base(id)
        {
            Nome = nome;
            Marca = marca;
            Descrizione = descrizione;
            Prezzo = prezzo;
            Categoria = categoria;
            Genere = genere;
            Sconto = sconto;
            Quantita = quantita;
            Taglia = taglia;
            Immagine1 = immagine1;
            Immagine2 = immagine2;
            Immagine3 = immagine3;
            Immagine4 = immagine4;
            Colore = colore;
            Sku = sku;
        }

        #region Properties
        private byte[] _immagine1;
        private byte[] _immagine2;
        private byte[] _immagine3;
        private byte[] _immagine4;
        private string? _sku;
        private string _colore;

        public byte[] Immagine1 { get => _immagine1; set => _immagine1 = value; }
        public byte[] Immagine2 { get => _immagine2; set => _immagine2 = value; }
        public byte[] Immagine3 { get => _immagine3; set => _immagine3 = value; }
        public byte[] Immagine4 { get => _immagine4; set => _immagine4 = value; }

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
                    _colore = "0";
            }
        }

        public string? Sku
        {
            get => _sku;
            set
            {
                _sku = value;

                if (Immagine1 != null)
                {
                    _sku += "1";
                }
                else
                {
                    _sku += "0";
                }
                if (Immagine2 != null)
                {
                    _sku += "1";
                }
                else
                {
                    _sku += "0";
                }
                if (Immagine3 != null)
                {
                    _sku += "1";
                }
                else
                {
                    _sku += "0";
                }
                if (Immagine4 != null)
                {
                    _sku += "1";
                }
                else
                {
                    _sku += "0";
                }
            }
        }

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
                        _nome = "#";
                    }
                    else
                    {
                        _nome = value;
                        _nome.Replace("'", "''");
                    }
                }
                else
                    _nome = "0";
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
                        _marca = "#";
                    }
                    else
                    {
                        _marca = value;
                        _marca.Replace("'", "''");
                    }
                }
                else
                    _marca = "0";
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
                        _descrizione = "#";
                    else
                    {
                        _descrizione = value;
                        _descrizione.Replace("'", "''");
                    }
                }
                else
                    _descrizione = "0";
            }
        }

        private double? _prezzo;
        public double? Prezzo 
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
         
        private string? _categoria;
        public string? Categoria 
        {
            get => _categoria;
            set
            {
                if (value != null)
                {
                    string stagione = "inverno estate primavera autunno winter summer spring autumn fall";

                    if (!stagione.Contains(value.ToLowerInvariant()))
                    {
                        _categoria = "#";
                    }
                    else
                    {
                        _categoria = value;
                        _categoria.Replace("'", "''");
                    }
                }
                else
                    _categoria = "0";
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
                    if (!generi.Contains(value.ToLowerInvariant()))
                        _genere = "#";
                    else
                    {
                        _genere = value;
                        _genere.Replace("'", "''");
                    }
                }
                else
                    _genere = "0";

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
        #endregion
    }
}