using SandalProject.Utility;

namespace SandalProject.Models
{
    public class DAOSandali : IDAO
    {
        private Database db;

        #region singleton
        private static DAOSandali instance = null;

        private DAOSandali()
        {
            db = Database.GetInstance();
        }

        public static DAOSandali GetInstance()
        {
            if (instance == null)
                instance = new DAOSandali();
            return instance;
        }
        #endregion

        public bool Delete(int id)
        {
            bool d = false;
            try
            {
                d = db.Update($"delete from Sandali where id = {id}");
            }
            catch
            {
                throw new AbandonedMutexException("Errore rimozione Oggetto");
            }
            return d;
        }

        public bool DeleteFromSku(int sku_DB)
        {
            bool d = false;
            try
            {
                d = db.Update($"delete from Sandali WHERE sku LIKE '{sku_DB}[^0-9]%';");
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
                riga = db.ReadOne($"Select * from Sandali where id = {id};");
            }
            catch
            {
                throw new ArgumentException("Errore oggetto non esistente");
            }

            Sandali s = new();

            if (riga != null)
            {
                s.FromDictionary(riga);
            }
            return s;
        }

        public bool FindSkuBool(string sku)
        {
            Dictionary<string, string> riga = null;

            bool find = false;

            try
            {
                riga = db.ReadOne($"Select * from Sandali where sku = {sku};");
            }
            catch
            {
                throw new ArgumentException("Errore oggetto non esistente");
            }

            Sandali s = new();

            if (riga != null)
            {
                s.FromDictionary(riga);
                find = true;
            }

            return find;
        }

        public bool Insert(Entity e)
        {
            Sandali s = (Sandali)e;
            bool i = false;

            byte[]? immagineHex1 = s.Immagine1 != null && s.Immagine1.Length > 0
                ? s.Immagine1
                : null; // Lasciamo null se l'immagine è vuota o non esiste

            System.Data.SqlTypes.SqlBinary immagineSqlBinary1 = immagineHex1 != null
                ? new System.Data.SqlTypes.SqlBinary(immagineHex1)
                : System.Data.SqlTypes.SqlBinary.Null;

            byte[]? immagineHex2 = s.Immagine2 != null && s.Immagine2.Length > 0
                ? s.Immagine2
                : null; // Lasciamo null se l'immagine è vuota o non esiste

            System.Data.SqlTypes.SqlBinary immagineSqlBinary2 = immagineHex2 != null
                ? new System.Data.SqlTypes.SqlBinary(immagineHex2)
                : System.Data.SqlTypes.SqlBinary.Null;

            byte[]? immagineHex3 = s.Immagine3 != null && s.Immagine3.Length > 0
                ? s.Immagine3
                : null; // Lasciamo null se l'immagine è vuota o non esiste

            System.Data.SqlTypes.SqlBinary immagineSqlBinary3 = immagineHex3 != null
                ? new System.Data.SqlTypes.SqlBinary(immagineHex3)
                : System.Data.SqlTypes.SqlBinary.Null;

            byte[]? immagineHex4 = s.Immagine4 != null && s.Immagine4.Length > 0
                ? s.Immagine4
                : null; // Lasciamo null se l'immagine è vuota o non esiste

            System.Data.SqlTypes.SqlBinary immagineSqlBinary4 = immagineHex4 != null
                ? new System.Data.SqlTypes.SqlBinary(immagineHex4)
                : System.Data.SqlTypes.SqlBinary.Null;

            string? colore = s.Colore;
            string nome = s.Nome == null ? "NULL" : $"'{s.Nome.Replace("'", "''")}'";
            string marca = s.Marca == null ? "NULL" : $"'{s.Marca.Replace("'", "''")}'";
            string descrizione = s.Descrizione == null ? "NULL" : $"'{s.Descrizione.Replace("'", "''")}'";
            double? prezzo = s.Prezzo;
            string? sku = s.Sku;
            string categoria = s.Categoria == null ? "NULL" : $"'{s.Categoria.Replace("'", "''")}'";
            string genere = s.Genere == null ? "NULL" : $"'{s.Genere.Replace("'", "''")}'";
            double? sconto = s.Sconto;
            int? quantita = s.Quantita;
            int? taglia = s.Taglia;

            try
            {
                string query = "INSERT INTO Sandali (Nome, Marca, Descrizione, Prezzo, SKU, Categoria, Genere, Sconto, Quantita, Taglia, immagine1, immagine2, immagine3, immagine4, Colore) " +
                                    "VALUES (@nome, @marca, @descrizione, @prezzo, @Sku, @categoria, @genere, @sconto, @quantita, @taglia, @Immagine1, @Immagine2, @Immagine3, @Immagine4, @Colore)";

                db.InsertSandalo(nome, marca, query, descrizione, prezzo, categoria, genere, sconto, quantita, taglia, immagineSqlBinary1, immagineSqlBinary2, immagineSqlBinary3, immagineSqlBinary4, sku, colore);
            }
            catch
            {
                throw new ArgumentException("Errore nell'inserimento");
            }
            return i;
        }

        public List<Entity> ReadAll()
        {
            List<Entity> lista = new();

            List<Dictionary<string, string>> righe = null;
            try
            {
                righe = db.Read("Select * from Sandali");
            }
            catch
            {
                throw new ArgumentException("errore nella lettura");
            }

            if (righe != null)
                foreach (var r in righe)
                {
                    Sandali a = new();
                    a.FromDictionary(r);
                    //Console.WriteLine("Genere nel ReadAll " + a.Genere);
                    lista.Add(a);
                }

            return lista;
        }

        public bool Update(Entity e)
        {
            Sandali s = (Sandali)e;
            bool updated = false;
            // Conversione dell'immagine in esadecimale
            string immagineHex1 = "0x" + BitConverter.ToString(s.Immagine1).Replace("-", "");
            string immagineHex2 = "0x" + BitConverter.ToString(s.Immagine2).Replace("-", "");
            string immagineHex3 = "0x" + BitConverter.ToString(s.Immagine3).Replace("-", "");
            string immagineHex4 = "0x" + BitConverter.ToString(s.Immagine4).Replace("-", "");

            try
            {
                updated = db.Update($"UPDATE Sandali SET " +
                                     $"Nome = {(s.Nome == null ? "NULL" : $"'{s.Nome.Replace("'", "''")}'")}, " +
                                     $"Marca = {(s.Marca == null ? "NULL" : $"'{s.Marca.Replace("'", "''")}'")}, " +
                                     $"Descrizione = {(s.Descrizione == null ? "NULL" : $"'{s.Descrizione.Replace("'", "''")}'")}, " +
                                     $"Prezzo = {(s.Prezzo == 0 ? 0 : s.Prezzo)}, " +
                                     $"SKU = {(s.Sku == null ? "NULL" : $"'{s.Sku.Replace("'", "''")}'")}, " +
                                     $"Categoria = {(s.Categoria == null ? "NULL" : $"'{s.Categoria.Replace("'", "''")}'")}, " +
                                     $"Genere = {(s.Genere == null ? "NULL" : $"'{s.Genere.Replace("'", "''")}'")}, " +
                                     $"Sconto = {(s.Sconto == 0 ? 0 : s.Sconto)}, " +
                                     $"Quantita = {s.Quantita}, " +
                                     $"Taglia = {(s.Taglia == 0 ? 0 : s.Taglia)} " +
                                     $"Immagine1 = {immagineHex1}, " +
                                     $"Immagine2 = {immagineHex2}, " +
                                     $"Immagine3 = {immagineHex3}, " +
                                     $"Immagine4 = {immagineHex4}, " +
                                     $"Colore = {(s.Colore == null ? "NULL" : $"'{s.Colore.Replace("'", "''")}'")} " +
                                     $"WHERE id = {s.Id};");
            }
            catch
            {
                throw new ArgumentException("Errore nell'aggiornamento");
            }

            return updated;
        }

        public bool UpdateFromSku(Entity e, int sku_Excel)
        {
            Sandali s = (Sandali)e;
            bool updated = false;
            string immagineHex1 = "0x" + BitConverter.ToString(s.Immagine1).Replace("-", "");
            string immagineHex2 = "0x" + BitConverter.ToString(s.Immagine2).Replace("-", "");
            string immagineHex3 = "0x" + BitConverter.ToString(s.Immagine3).Replace("-", "");
            string immagineHex4 = "0x" + BitConverter.ToString(s.Immagine4).Replace("-", "");
            try

            {
                updated = db.Update($"UPDATE Sandali SET " +
                                     $"Nome = {(s.Nome == null ? "NULL" : $"'{s.Nome.Replace("'", "''")}'")}, " +
                                     $"Marca = {(s.Marca == null ? "NULL" : $"'{s.Marca.Replace("'", "''")}'")}, " +
                                     $"Descrizione = {(s.Descrizione == null ? "NULL" : $"'{s.Descrizione.Replace("'", "''")}'")}, " +
                                     $"Prezzo = {(s.Prezzo == 0 ? 0 : s.Prezzo)}, " +
                                     $"SKU = {(s.Sku == null ? "NULL" : $"'{s.Sku.Replace("'", "''")}'")}, " +
                                     $"Categoria = {(s.Categoria == null ? "NULL" : $"'{s.Categoria.Replace("'", "''")}'")}, " +
                                     $"Genere = {(s.Genere == null ? "NULL" : $"'{s.Genere.Replace("'", "''")}'")}, " +
                                     $"Sconto = {(s.Sconto == 0 ? 0 : s.Sconto)}, " +
                                     $"Quantita = {s.Quantita}, " +
                                     $"Taglia = {(s.Taglia == 0 ? 0 : s.Taglia)} " +
                                     $"Immagine1 = {immagineHex1}, " +
                                     $"Immagine2 = {immagineHex2}, " +
                                     $"Immagine3 = {immagineHex3}, " +
                                     $"Immagine4 = {immagineHex4}, " +
                                     $"Colore = {(s.Colore == null ? "NULL" : $"'{s.Colore.Replace("'", "''")}'")} " +
                                     $"WHERE SKU LIKE '{sku_Excel}[^0-9]%';");

            }
            catch
            {
                throw new ArgumentException("Errore nell'aggiornamento");
            }

            return updated;
        }

        //public Sandali SandaloDelMese(string stagione)
        //{
        //    var riga = new Dictionary<string, string>();
        //    Sandali s = new();
        //    try
        //    {
        //        riga = db.ReadOne($@"SELECT TOP 1 s.*
        //                                 FROM Sandali s
        //                                 WHERE RIGHT(s.SKU, 4) LIKE '%1%'
        //                                 AND s.SKU IN (
        //                                     SELECT TOP 1 w.skuSandali 
        //                                     FROM Wishlist w
        //                                     JOIN Sandali s2 ON s2.id = w.idSandali 
        //                                     WHERE s2.categoria = '{stagione}' 
        //                                     GROUP BY w.skuSandali 
        //                                     ORDER BY COUNT(w.idSandali) DESC
        //                                 );");

                
        //        s.FromDictionary(riga);
        //    }
        //    catch
        //    {
        //        throw new ArgumentException("errore sandalo del mese");
        //    }

        //    return s;
        //}

        public int GetId()
        {
            Dictionary<string, string> riga = null;

            try
            {
                riga = db.ReadOne("SELECT * FROM Sandali WHERE Id IN(SELECT MAX(Id) FROM Sandali);");
            }
            catch
            {
                throw new ArgumentException("errore id non trovato");
            }

            Account account = new Account();

            if (riga != null)
                account.FromDictionary(riga);

            return account.Id;
        }

        public bool AggiornaTabella()
        {
            bool updated = false;
            bool updated2 = false;

            try
            {
                updated = db.Update($"DELETE FROM Sandali;");
                updated2 = db.Update("DBCC CHECKIDENT ('Sandali', RESEED, 0);");
            }
            catch
            {
                throw new ArgumentException("Errore nell'aggiornamento");
            }

            return updated;
        }

        public List<Sandali> CercaColore(string sku, int id)
        {
            List<Sandali> lista = new();
            List<Dictionary<string, string>> righe = null;

            try
            {
                righe = db.Read($"SELECT * FROM Sandali WHERE Left(sku,Len(sku)-4) = '{sku}' Order by case When id = {id} then 0 else 1 END;");

            }
            catch
            {
                throw new ArgumentException("Errore nell'aggiornamento");
            }

            if (righe != null)
            {
                foreach (var r in righe)
                {
                    Sandali a = new();
                    a.FromDictionary(r);
                    lista.Add(a);
                }
            }

            return lista;
        }
    }
}
