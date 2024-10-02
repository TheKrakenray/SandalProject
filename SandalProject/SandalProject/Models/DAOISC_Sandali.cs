using SandalProject.Utility;
using System.Data.SqlClient;
using System.Data;

namespace SandalProject.Models
{
    public class DAOISC_Sandali : IDAO
    {
        private Database db;

        #region singleton
        private static DAOISC_Sandali instance = null;

        private DAOISC_Sandali()
        {
            db = Database.GetInstance();
        }

        public static DAOISC_Sandali GetInstance()
        {
            if (instance == null)
                instance = new DAOISC_Sandali();
            return instance;
        }
        #endregion

        public bool Delete(int id)
        {
            bool d = false;

            try
            {
                d = db.Update($"delete from ISC_Sandali where id = {id}");
            }
            catch
            {
                throw new AbandonedMutexException("Errore rimozione Oggetto");
            }

            return d;
        }

        public Entity Find(int id)
        {
            Dictionary<string, string> riga = null;
            try
            {
                riga = db.ReadOne($"select * from ISC_Sandali where id = {id};");
            }
            catch
            {
                throw new ArgumentException("Errore Uh SANDAL non esistente");
            }

            Account account = new Account();

            if (riga != null)
                account.FromDictionary(riga);

            return account;
        }

        public bool Insert(Entity e)
        {
            ISC_Sandali sandali = (ISC_Sandali)e;
            bool ins = false;

            byte[]? immagineHex1 = sandali.Immagine1 != null && sandali.Immagine1.Length > 0
                ? sandali.Immagine1
                : null; // Lasciamo null se l'immagine è vuota o non esiste

            System.Data.SqlTypes.SqlBinary immagineSqlBinary1 = immagineHex1 != null
                ? new System.Data.SqlTypes.SqlBinary(immagineHex1)
                : System.Data.SqlTypes.SqlBinary.Null;

            byte[]? immagineHex2 = sandali.Immagine2 != null && sandali.Immagine2.Length > 0
                ? sandali.Immagine2
                : null; // Lasciamo null se l'immagine è vuota o non esiste

            System.Data.SqlTypes.SqlBinary immagineSqlBinary2 = immagineHex2 != null
                ? new System.Data.SqlTypes.SqlBinary(immagineHex2)
                : System.Data.SqlTypes.SqlBinary.Null;

            byte[]? immagineHex3 = sandali.Immagine3 != null && sandali.Immagine3.Length > 0
                ? sandali.Immagine3
                : null; // Lasciamo null se l'immagine è vuota o non esiste

            System.Data.SqlTypes.SqlBinary immagineSqlBinary3 = immagineHex3 != null
                ? new System.Data.SqlTypes.SqlBinary(immagineHex3)
                : System.Data.SqlTypes.SqlBinary.Null;

            byte[]? immagineHex4 = sandali.Immagine4 != null && sandali.Immagine4.Length > 0
                ? sandali.Immagine4
                : null; // Lasciamo null se l'immagine è vuota o non esiste

            System.Data.SqlTypes.SqlBinary immagineSqlBinary4 = immagineHex4 != null
                ? new System.Data.SqlTypes.SqlBinary(immagineHex4)
                : System.Data.SqlTypes.SqlBinary.Null;

            string? sku = sandali.Sku;
            string? colore = sandali.Colore;

            try
            {
                string query = "INSERT INTO ISC_Sandali (immagine1, immagine2, immagine3, immagine4, Sku, Colore) " +
                                   "VALUES (@Immagine1, @Immagine2, @Immagine3, @Immagine4, @Sku, @Colore)";

                db.InsertISC(query, immagineSqlBinary1, immagineSqlBinary2, immagineSqlBinary3, immagineSqlBinary4, sku, colore);
            }
            catch
            {
                throw new ArgumentException("Errore nell'inserimento");
            }

            return ins;
        }

        public List<Entity> ReadAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Entity e)
        {
            ISC_Sandali sandali = (ISC_Sandali)e;
            bool updated = false;
            // Conversione dell'immagine in esadecimale
            string immagineHex1 = "0x" + BitConverter.ToString(sandali.Immagine1).Replace("-", "");
            string immagineHex2 = "0x" + BitConverter.ToString(sandali.Immagine2).Replace("-", "");
            string immagineHex3 = "0x" + BitConverter.ToString(sandali.Immagine3).Replace("-", "");
            string immagineHex4 = "0x" + BitConverter.ToString(sandali.Immagine4).Replace("-", "");
            try
            {
                // Query di aggiornamento solo per Immagine, Sku e Colore
                updated = db.Update($"UPDATE ISC_Sandali SET " +
                                    $"Immagine1 = {immagineHex1}, " +
                                    $"Immagine2 = {immagineHex2}, " +
                                    $"Immagine3 = {immagineHex3}, " +
                                    $"Immagine4 = {immagineHex4}, " +
                                    $"Sku = {(sandali.Sku == null ? "NULL" : $"'{sandali.Sku.Replace("'", "''")}'")}, " +
                                    $"Colore = {(sandali.Colore == null ? "NULL" : $"'{sandali.Colore.Replace("'", "''")}'")}, " +
                                    $"WHERE Sku = {sandali.Sku};");
            }
            catch
            {
                throw new ArgumentException("Errore nell'aggiornamento");
            }

            return updated;
        }

        public bool Update(Entity e,int numeroImg)
        {
            ISC_Sandali sandali = (ISC_Sandali)e;
            bool updated = false;
            // Conversione dell'immagine in esadecimale
            string immagineHex1 = "0x" + BitConverter.ToString(sandali.Immagine1).Replace("-", "");

            try
            {
                // Query di aggiornamento solo per Immagine, Sku e Colore
                updated = db.Update($"UPDATE ISC_Sandali SET " +
                                    $"Immagine'{numeroImg}' = {immagineHex1}, " +
                                    $"WHERE Sku = {sandali.Sku};");
            }
            catch
            {
                throw new ArgumentException("Errore nell'aggiornamento");
            }

            return updated;
        }

        public bool AggiornaTabella()
        {
            bool updated = false;
            bool updated2 = false;

            try
            {
                updated = db.Update($"Delete from ISC_Sandali;");
            }
            catch
            {
                throw new ArgumentException("Errore nell'aggiornamento");
            }

            return updated;
        }
    }
}
