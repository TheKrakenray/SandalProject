﻿using SandalProject.Utility;
using System.Reflection;
using System.Runtime.CompilerServices;

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


        public Entity Find(string username)
        {
            Dictionary<string, string> riga=null;
            try
            {
                riga = db.ReadOne($"select * from account where username = {username}");
            }
            catch
            {
                throw new ArgumentException("Errore account non esistente");
            }

            Account a = new();
            if(riga != null)
            {
                a.FromDictionary(riga);
            }
            return a;
        }

        public Entity Find(int id)
        {
            Dictionary<string, string> riga = null;
            try
            {
                riga = db.ReadOne($"select * from account where id = {id};");
                foreach (KeyValuePair<string, string> accoppiata in riga)
                {
                    Console.WriteLine($"Chiave: {accoppiata.Key} --- Valore: {accoppiata.Value}");
                }
            }
            catch
            {
                throw new ArgumentException("Errore account non esistente");
            }

            Account account = new Account();
            StampaProprietà<Account>(account);
            account.Id = null;
            if(riga != null)
                account.FromDictionary(riga);


            return account;
        }

        static void StampaProprietà<T>(T obj)
        {
            Type tipo = obj.GetType();
            PropertyInfo[] proprietà = tipo.GetProperties();

            foreach (var prop in proprietà)
            {
                var valore = prop.GetValue(obj);
                Console.WriteLine($"{prop.Name}: {valore}");
            }
        }

        public bool Insert(Entity e)
        {
            Account a = (Account)e;
            bool ins = false;
            try
            {
                ins = db.Update($"Insert into Account " +
                             $"(Propic, Username, Email, Psw, Ruolo, PFedelta) " +
                             $"Values " +
                             $"((SELECT CONVERT(varbinary, '{a.Propic}'))," +
                             $"'{(a.Username == null ? "null" : a.Username.Replace("'", "''"))}'," +
                             $"'{(a.Email == null ? "null" : a.Email.Replace("'", "''"))}'," +
                             $"HASHBYTES('SHA2_512','{(a.Psw == null ? "null" : a.Psw.Replace("'", "''"))}')," +
                             $"'{(a.Ruolo == null ? "null" : a.Ruolo.Replace("'", "''"))}'," +
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

            List<Dictionary<string,string>> righe = null;
            try
            {
                if(Valida("admin",AdminPsw))
                    righe = db.Read("Select Account.username,Account.email,Account.ruolo,Account.Pfedelta from Account");
            }
            catch
            {
                throw new ArgumentException("errore nella lettura");
            }

            if(righe!=null)
            foreach(var r in righe)
            {
                Account a = new();
                a.FromDictionary(r);
                lista.Add(a);
            }

            return lista;
        }

        public bool Update(Entity e)
        {
            Account a = new();
            bool upd = false;
            try
            {
                upd = db.Update($"UPDATE account SET " +
                                 $"Propic = ((SELECT CONVERT(varbinary, '{a.Propic}'))," +
                                 $"Username = '{(a.Username == null ? "null" : a.Username.Replace("'", "''"))}' " +
                                 $"Email = '{(a.Email == null ? "null" : a.Email.Replace("'","''"))}'," +
                                 $"Password = HASHBYTES('SHA2_512','{(a.Psw == null ? "null" : a.Psw.Replace("'", "''"))}')," +
                                 $"Ruolo = '{(a.Ruolo == null ? "null" : a.Ruolo.Replace("'", "''"))}'," +
                                 $"PFedelta = {a.PFedelta}," +
                                 $"WHERE id = {a.Id}");
            }
            catch
            {
                throw new ArgumentException("Errore modifica account");
            }
            return upd;
        }

        public bool Valida(string username_email, string password)
        {
            Dictionary<string, string> riga = null;
            try
            {
                riga = db.ReadOne($"SELECT account.username FROM account WHERE (username = '{(username_email.Replace("'", "''"))}' OR email = '{(username_email.Replace("'", "''"))}') AND passw = HASHBYTES('SHA2_512','{(password.Replace("'", "''"))}');");
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
    }
}
