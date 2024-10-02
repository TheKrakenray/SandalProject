using SandalProject.Utility;
using System.Text.RegularExpressions;

namespace SandalProject.Models
{
    public class ISC_Sandali : Entity
    {
        private byte[] _immagine1;
        private byte[] _immagine2;
        private byte[] _immagine3;
        private byte[] _immagine4;
        private string? _sku;
        private string _colore;

        public ISC_Sandali() { }

        public ISC_Sandali(int id, byte[] immagine1, byte[] immagine2, byte[] immagine3, byte[] immagine4, string colore, string sku) : base(id)
        {
            Immagine1 = immagine1;
            Immagine2 = immagine2;
            Immagine3 = immagine3;
            Immagine4 = immagine4;
            Colore = colore;
            Sku = sku;
        }

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
                    _sku +="0";
                }
                if(Immagine2 != null)
                {
                    _sku += "1";
                }
                else
                {
                    _sku += "0";
                }
                if(Immagine3 != null)
                {
                    _sku += "1";
                }
                else
                {
                    _sku += "0";
                }
                if(Immagine4 != null)
                {
                    _sku += "1";
                }
                else
                {
                    _sku += "0";
                }
            }
        }
    }
}
