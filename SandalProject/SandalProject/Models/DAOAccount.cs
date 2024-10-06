using SandalProject.Utility;
using System.Reflection;
using System.Runtime.CompilerServices;
using SandalProject.Controllers;
namespace SandalProject.Models
{
    public class DAOAccount : IDAO
    {
        private Database db;

        #region singleton
        private static DAOAccount instance = null;

        private DAOAccount()
        {
            db = Database.GetInstance();
        }

        public static DAOAccount GetInstance()
        {
            if (instance == null)
                instance = new DAOAccount();
            return instance;
        }
        #endregion

        public bool Delete(int id)
        {
            bool del = false;
            try
            {
                del = db.Update($"delete from Account where id = {id}");
            }
            catch
            {
                throw new ArgumentException("Errore rimozione account");
            }
            return del;
        }

        public Entity Find(string email)
        {
            Dictionary<string, string> riga = null;
            try
            {
                riga = db.ReadOne($"select * from account where email = '{email}'");
            }
            catch
            {
                throw new ArgumentException("Errore account non esistente");
            }

            Account a = new();
            if (riga != null)
            {
                a.FromDictionary(riga);
                return a;
            }
            else
            {
                return null;
            }
        }

        public Entity Find(int id)
        {
            Dictionary<string, string> riga = null;
            try
            {
                riga = db.ReadOne($"select * from account where id = {id};");
            }
            catch
            {
                throw new ArgumentException("Errore account non esistente");
            }

            Account account = new Account();

            if(riga != null)
                account.FromDictionary(riga);

            return account;
        }

        public bool Insert(Entity e)
        {
            Account a = (Account)e;
            bool ins = false;
            string propic = $"0x{BitConverter.ToString(a.Propic).Replace("-", "")}";

            try
            {
                ins = db.Update($"INSERT INTO Account " +
                                $"(Propic, Username, Email, Psw, Ruolo, PFedelta) " +
                                $"VALUES " +
                                $"({propic}, " +  // Assicurati che 'propic' sia corretto
                                $"{(a.Username == null ? "NULL" : $"'{a.Username.Replace("'", "''")}'")}, " +
                                $"{(a.Email == null ? "NULL" : $"'{a.Email.Replace("'", "''")}'")}, " +
                                $"HASHBYTES('SHA2_512', {(a.Psw == null ? "NULL" : $"'{a.Psw.Replace("'", "''")}'")}), " +
                                $"{(a.Ruolo == null ? "NULL" : $"'{a.Ruolo.Replace("'", "''")}'")}, " +
                                $"{a.PFedelta});");
            }
            catch
            {
                throw new ArgumentException("Errore nell'inserimento");
            }

            return ins;
        }

        public List<Entity> ReadAll()
        {
            throw new ArgumentException("funzione deprecata");
        }

        public List<Entity> ReadAll(string AdminPsw)
        {
            List<Entity> lista = new();

            List<Dictionary<string, string>> righe = null;
            try
            {
                if (Valida("admin", AdminPsw))
                    righe = db.Read("Select Account.username,Account.email,Account.ruolo,Account.Pfedelta from Account");
            }
            catch
            {
                throw new ArgumentException("errore nella lettura");
            }

            if (righe != null)
                foreach (var r in righe)
                {
                    Account a = new();
                    a.FromDictionary(r);
                    lista.Add(a);
                }

            return lista;
        }

        public bool Update(Entity e)
        {
            Account a = (Account)e;
            bool updated = false;
            string propicHex = "0x" + BitConverter.ToString(a.Propic).Replace("-", "");

            try
            {
                updated = db.Update($"UPDATE Account SET " +
                                    $"Propic = {propicHex}, " +
                                    $"Username = {(a.Username == null ? "NULL" : $"'{a.Username.Replace("'", "''")}'")}, " +
                                    $"Email = {(a.Email == null ? "NULL" : $"'{a.Email.Replace("'", "''")}'")}, " +
                                    $"Psw = HASHBYTES('SHA2_512', {(a.Psw == null ? "NULL" : $"'{a.Psw.Replace("'", "''")}'")}), " +
                                    $"Ruolo = {(a.Ruolo == null ? "NULL" : $"'{a.Ruolo.Replace("'", "''")}'")}, " +
                                    $"PFedelta = {a.PFedelta} " +
                                    $"WHERE Id = {a.Id};");
            }
            catch
            {
                throw new ArgumentException("Errore nell'aggiornamento");
            }

            return updated;
        }

        public bool ChangeImgDb(int id, byte[] Img)
        {
            bool updated = false;
            string propicHex = "0x" + BitConverter.ToString(Img).Replace("-", "");

            try
            {
                updated = db.Update($"UPDATE Account SET " +
                                    $"Propic = {propicHex} " +
                                    $"Where id = {id}");
            }
            catch
            {
                throw new ArgumentException("Errore nell'aggiornamento");
            }

            return updated;
        }

        public bool Valida(string username_email, string password)
        {
            Dictionary<string, string> riga = null;
            try
            {
                riga = db.ReadOne($"SELECT account.email FROM account WHERE email = '{(username_email.Replace("'", "''"))}' AND psw = HASHBYTES('SHA2_512','{(password.Replace("'", "''"))}');");
            }
            catch
            {
                throw new ArgumentException("errore nella validazione");
            }
            if (riga != null)
                return true;
            else
                return false;
        }

        public Entity MaxId()
        {
            Dictionary<string, string> riga = null;

            try
            {
                riga = db.ReadOne("SELECT * FROM Account WHERE Id IN(SELECT MAX(Id) FROM Account);");
            }
            catch
            {
                throw new ArgumentException("errore id non trovato");
            }

            Account account = new Account();

            if (riga != null)
                account.FromDictionary(riga);

            return account;
        }

        #region Carrello
        public List<Sandali> GetCarrello(Account utente)
        {
            List<Sandali> san = new();
            List<Dictionary<string, string>> righe = new();
            try
            {
                righe = db.Read($"select Sandali.* from Carrello join Sandali on IdSandali = Sandali.Id where IdAccount = {utente.Id}");
            }
            catch
            {
                Console.WriteLine("Errore nel riempimento in tabella di utente id " + utente.Id);
            }
            foreach (var r in righe)
            {
                Sandali s = new();
                s.FromDictionary(r);
                san.Add(s);
            }

            return san;
        }

        public bool ResetCarrello(Account utente)
        {
            try
            {
                return db.Update($"Delete from Carrello where IdAccount = {utente.Id} ");
            }
            catch
            {
                Console.WriteLine("Errore nel reset in tabella di utente id " + utente.Id);
                return false;
            }
        }

        public bool RemoveCarrello(Account utente, Sandali s)
        {
            try
            {
                return db.Update($"Delete from Carrello where IdAccount = {utente.Id} AND IdSandali = {s.Id} ");
            }
            catch
            {
                Console.WriteLine("Errore nell'eliminazione in tabella di prodotto " + s.Id + "  di utente id " + utente.Id);
                return false;
            }
        }

        public bool AddCarrello(Account utente, Sandali s)
        {
            try
            {
                return db.Update($"Insert into Carrello " +
                                $"(IdAccount, IdSandali) " +
                                $"Values " +
                                $"({utente.Id}, {s.Id});");
            }
            catch
            {
                Console.WriteLine("Errore nell'inserimento in tabella di prodotto " + s.Id + "  di utente id " + utente.Id);
                return false;
            }
        }
        #endregion

        #region WList
        public List<Sandali> GetWList(Account utente)
        {
            List<Sandali> san = new();
            List<Dictionary<string, string>> righe = new();
            try
            {
                righe = db.Read($"select Sandali.* from Wishlist join Sandali on IdSandali = Sandali.Id where IdAccount = {utente.Id}");
            }
            catch
            {
                Console.WriteLine("Errore nel riempimento in tabella di utente id " + utente.Id);
            }
            foreach (var r in righe)
            {
                Sandali s = new();
                s.FromDictionary(r);
                san.Add(s);
            }

            return san;
        }

        public bool FindSandaloWList(Account utente, int id)
        {
            List<Sandali> san = new();
            List<Dictionary<string, string>> righe = new();

            try
            {
                righe = db.Read($"Select * from Wishlist where IdAccount = {utente.Id} AND IdSandali = {id} ;");
            }
            catch
            {
                Console.WriteLine("Errore nel find in tabella WList id " + id);
                return false;
            }

            foreach (var r in righe)
            {
                Sandali s = new();
                s.FromDictionary(r);
                san.Add(s);
            }

            if (san.Count > 0)
            {
                return true;
            }

            return false;
        }

        public bool ResetWList(Account utente)
        {
            try
            {
                return db.Update($"Delete from Wishlist where IdAccount = {utente.Id} ");
            }
            catch
            {
                Console.WriteLine("Errore nel reset in tabella di utente id " + utente.Id);
                return false;
            }
        }

        public bool RemoveWList(Account utente, Sandali s)
        {
            try
            {
                return db.Update($"Delete from Wishlist where IdAccount = {utente.Id} AND IdSandali = {s.Id} ");
            }
            catch
            {
                Console.WriteLine("Errore nell'eliminazione in tabella di prodotto " + s.Id + "  di utente id " + utente.Id);
                return false;
            }
        }

        public bool AddWList(Account utente, Sandali s)
        {
            try
            {
                return db.Update($"Insert into Wishlist " +
                                $"(IdAccount, IdSandali, skuSandali) " +
                                $"Values " +
                                $"({utente.Id}, {s.Id}, '{s.Sku.Substring(0,s.Sku.Length - 4)}');");
            }
            catch
            {
                Console.WriteLine("Errore nell'inserimento in tabella di prodotto " + s.Id + "  di utente id " + utente.Id);
                return false;
            }
        }
        #endregion
    }
}
