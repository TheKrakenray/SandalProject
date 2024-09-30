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

        public bool Insert(Entity e)
        {
            Sandali s = (Sandali)e;
            bool i = false;
            try
            {
                i = db.Update($"Insert into Sandali " +
                             $"(Nome, Marca, Descrizione, Prezzo, SKU, Categoria, Genere, Sconto, Quantita, Taglie, Colore, Immagine) " +
                             $"Values " +
                             $"({(s.Nome == null ? "NULL" : $"'{s.Nome.Replace("'", "''")}'")}," +
                             $"{(s.Marca == null ? "NULL" : $"'{s.Marca.Replace("'", "''")}'")}," +
                             $"{(s.Descrizione == null ? "NULL" : $"'{s.Descrizione.Replace("'", "''")}'")}," +
                             $"{s.Prezzo}," +
                             $"{(s.SKU == null ? "NULL" : $"'{s.SKU.Replace("'", "''")}'")}," +
                             $"{(s.Categoria == null ? "NULL" : $"'{s.Categoria.Replace("'", "''")}'")}," +
                             $"{(s.Genere == null ? "NULL" : $"'{s.Genere.Replace("'", "''")}'")}," +
                             $"{s.Sconto}," +
                             $"{s.Quantita}," +
                             $"{s.Taglia}," +
                             $"{(s.Colore == null ? "NULL" : $"'{s.Colore.Replace("'", "''")}'")}," +
                             $"{s.Immagini});");
            }
            catch
            {
                throw new ArgumentException("Errore nell'inserimento");
            }
            return i;
            
            //int id, string nome, string marca, string descrizione, int prezzo, string sKU,
            //string categoria, string genere, double sconto, int quantita, int taglia, string colore, List<byte[]> immagini
        }

        public List<Entity> ReadAll()
        {
            List<Entity> lista = new();

            List<Dictionary<string, string>> righe = null;
            try
            {
                righe = db.Read("Select * from Account");
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
            Sandali s = new();
           return db.Update($"Update Sandali set"+
                $"{(s.Nome == null ? "NULL" : $"'{s.Nome.Replace("'", "''")}'")}," +
                $"{(s.Marca == null ? "NULL" : $"'{s.Marca.Replace("'", "''")}'")}," +
                $"{(s.Descrizione == null ? "NULL" : $"'{s.Descrizione.Replace("'", "''")}'")}," +
                $"{s.Prezzo}," +
                $"{(s.SKU == null ? "NULL" : $"'{s.SKU.Replace("'", "''")}'")}," +
                $"{(s.Categoria == null ? "NULL" : $"'{s.Categoria.Replace("'", "''")}'")}," +
                $"{(s.Genere == null ? "NULL" : $"'{s.Genere.Replace("'", "''")}'")}," +
                $"{s.Sconto}," +
                $"{s.Quantita}," +
                $"{s.Taglia}," +
                $"{(s.Colore == null ? "NULL" : $"'{s.Colore.Replace("'", "''")}'")}," +
                $"{s.Immagini};");
        }

        public Sandali SandaloDelMese(string stagione)
        {
            var riga = db.ReadOne($"SELECT * FROM Sandali WHERE SKU = (     SELECT TOP 1 s.SKU     FROM Sandali s     JOIN Wishlist w ON s.id = w.idSandali     WHERE s.categoria = '{stagione}'     GROUP BY s.SKU     ORDER BY COUNT(w.idSandali) DESC );");

            Sandali s = new();
            s.FromDictionary(riga);

            return s;
        }
    }
}
