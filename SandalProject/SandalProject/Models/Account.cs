using System.Net.Mail;
using SandalProject.Utility;
using System.Text.RegularExpressions;
namespace SandalProject.Models
{
    public class Account : Entity
    {
        public Account() { }
        public Account(int id, string username, string email, string password, string ruolo, int pFeledelta): base(id)
        {
            Username = username;
            Email = email;
            Password = password;
            Ruolo = ruolo;
            PFeledelta = pFeledelta;
            
        }
        public Account(int id, string username, string email, string password, string ruolo, int pFeledelta, byte[] propic) : base(id)
        {
            Username = username;
            Email = email;
            Password = password;
            Ruolo = ruolo;
            PFeledelta = pFeledelta;
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
                    Regex userRGX = new Regex(@"^[a-zA-Z0-9_-]{1,15}$");
                    //regex per l'user name può contenere caratteri normali e "-" o "_" , deve essere lungo fra 1 e 15 caratteri 
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
                if(value!=null)
                {
                    Regex emailRGX = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    //Regex per l'email: deve contenere un "@" può contenere per forza e massimo 2 punti uno all'inizio e uno dopo la chiocciola
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

        private string? _password;
        public string? Password 
        {
            get => _password;
            set
            {
                if(value == null)
                {
                    _password = "null";
                }
                else
                {
                    _password = value;
                }
            }
        }

        private string? _ruolo;
        public string? Ruolo 
        {
            get => _ruolo;
            set
            {
                if (value==null || value.ToLower() != "admin")
                    _ruolo = "user";
                else
                    _ruolo = value;
            }
        }

        private int? _pFedelta; 
        public int? PFeledelta 
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
