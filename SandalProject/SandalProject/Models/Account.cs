using System.Net.Mail;
using SandalProject.Utility;
using System.Text.RegularExpressions;
namespace SandalProject.Models
{
    public class Account : Entity
    {
        public Account() { }

        //public Account(int id, string username, string email, string psw, string ruolo, int pFedelta): base(id)
        //{
        //    Username = username;
        //    Email = email;
        //    Psw = psw;
        //    Ruolo = ruolo;
        //    PFedelta = pFedelta;
        //}

        public Account(int id, string username, string email, string psw, string ruolo, int pFedelta, byte[] propic) : base(id)
        {
            Username = username;
            Email = email;
            Psw = psw;
            Ruolo = ruolo;
            PFedelta = pFedelta;
            Propic = propic;
        }

        #region Properties
        private string? _username;
        public string? Username 
        {
            get => _username;
            set
            {
                if(value != null)
                {
                    Regex userRGX = new Regex(@"^[a-zA-Z0-9_-]{4,15}$");
                    //regex per l'user name può contenere caratteri normali e "-" o "_" , deve essere lungo fra 4 e 15 caratteri 
                    if (userRGX.Match(value).Success)
                        _username = value;
                    else
                        throw new ArgumentException("Invalid email format");
                }
                else
                {
                    _username = "null";
                }
            }
        }

        private string? _email;
        public string? Email 
        {
            get => _email;
            set
            {
                if (value != null)
                {
                    Regex emailRGX = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    //Regex per l'email: deve contenere una "@" e può contenere massimo 2 punti uno prima e uno dopo la "@"
                    //quindi abc.bla@email.com
                    if (emailRGX.Match(value).Success)
                        _email = value;
                    else
                        throw new ArgumentException("Invalid email format");
            }
                else
                {
                _email = "null";
            }
        }
        }

        private string? _psw;
        public string? Psw
        {
            get => _psw;
            set
            {
                if(value == null)
                {
                    _psw = "null";
                }
                else
                {
                    _psw = value;
                }
            }
        }

        private string? _ruolo;
        public string? Ruolo 
        {
            get => _ruolo;
            set
            {
                if (value == null || value.ToLower() != "admin")
                    _ruolo = "user";
                else
                    _ruolo = value;
            }
        }

        private int? _pFedelta; 
        public int? PFedelta 
        {   get => _pFedelta; 
            set
            {
                if (value != null)
                {
                    if(value < 0)
                    {
                        _pFedelta = 0;
                    }
                    else if(value > 500)
                    {
                        _pFedelta = 500;
                    }
                    else
                    {
                        _pFedelta = (int)value;
                    }
                }
                else
                {
                    _pFedelta = 0;
                }
            }
        }

        public byte[]? Propic { get; set; }
        #endregion
    }
}
