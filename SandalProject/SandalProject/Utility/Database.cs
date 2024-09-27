using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace SandalProject.Utility
{
    public class Database
    {

        private bool _creato = false;

        public bool Creato { get => _creato; }

        private SqlConnection Connection { get; set; }
        private static Database instance = null;

        private string nomeDB = "master";

        private Database(string nomeDB, string server = "localhost")
        {
            this.nomeDB = nomeDB;

            Connection = new SqlConnection($"Server={server};Integrated security=true;");
            //connessione al db master
            try
            {//controllo di esistenza del db richiesto
                Connection.Open();
                SqlCommand cmd = new SqlCommand($"USE {nomeDB}", Connection);
                cmd.ExecuteNonQuery();
                _creato = true;
            }
            catch (Exception ex)
            {//se il db non esiste, genera un eccezione, allora lo creiamo
                Console.WriteLine("creazione db in corso");
                string query = $"CREATE DATABASE {nomeDB}";
                SqlCommand cmd = new SqlCommand(query, Connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"USE {nomeDB}", Connection);
                cmd.ExecuteNonQuery();
                Connection.Close();
                //connessione al nostro db
                Connection = new SqlConnection($"Server={server};Database={nomeDB};Integrated security=true;");
                try
                { //creazione delle tabelle
                    CreaTabelle();
                }
                catch
                {
                    Console.WriteLine("Errore durante la creazione delle tabelle: " + ex.Message);
                }
                //il db non era già creato in precedenza
                _creato = false;
            }
            finally
            {
                Connection.Close();//precauzione
                                   //aggiorna la connessione da master a db corrente se necessario
                Connection = new SqlConnection($"Server=localhost;Database={nomeDB};Integrated security=true;");
            }
        }

        public static Database GetInstance()
        {
            if (instance == null)
                instance = new Database("ProjectSandal");

            return instance;
        }

        //private string connectionString = $"Server=localhost;Database=ProjectSandal;Integrated security=true;";

        //public byte[] GetImage(int id)
        //{
        //    byte[] imageData = null;

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        string sql = "SELECT Propic FROM Account WHERE Id = @id";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("@id", id);
        //        conn.Open();

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            imageData = (byte[])reader["Propic"];
        //        }
        //    }
        //    if (imageData == null || imageData.Length == 0)
        //    {
        //        Console.WriteLine("Errore: Immagine non trovata o vuota per l'ID specificato.");
        //    }
        //    // Return the image as a byte array (for display in frontend)
        //    Connection.Close();//precauzione

        //    return imageData;
        //}

        public List<Dictionary<string, string>> Read(string query)
        {
            List<Dictionary<string, string>> ris = new List<Dictionary<string, string>>();

            //Statistiche(Connection,query);
            Connection.Open();

            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Dictionary<string, string> riga = new Dictionary<string, string>();

                for (int i = 0; i < dr.FieldCount; i++)
                {
                    var columnName = dr.GetName(i).ToLower();
                    object columnValue = dr.GetValue(i);

                    if (columnValue is byte[] byteArray)
                    {
                        // Stampa i valori dell'array di byte
                        string byteValues = BitConverter.ToString(byteArray);
                        riga.Add(columnName, byteValues); // Puoi anche decidere di salvare come stringa
                    }
                    else
                    {
                        // Altri tipi di valore
                        riga.Add(columnName, columnValue.ToString());
                    }
                }

                ris.Add(riga);
            }

            dr.Close();
            Connection.Close();

            return ris;
        }


        public Dictionary<string, string> ReadOne(string query)
        {
            try
            {
                return Read(query)[0];
            }
            catch
            {
                return null;
            }
        }

        public bool Update(string query)
        {
            try
            {
                Connection.Open();
                //Console.WriteLine("connessione a:" + Connection.Database);

                SqlCommand cmd = new SqlCommand(query, Connection);
                //Statistiche(Connection,query);

                int affette = cmd.ExecuteNonQuery();

                return affette > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"\nQUERY ERRATA:\n{query}");

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Errore generico" + "\n" + e.Message);
                return false;
            }
            finally
            {
                Connection.Close();
            }
        }

        public void Statistiche(SqlConnection connection, string query = "")
        {
            Console.WriteLine($"{connection.State}\t {connection.Database}\t {connection.DataSource}\t{(query = query == "" ? "query non trovata" : query)}");
            Console.ReadKey();
        }

        public void DropDB(string query, string server = "localhost")
        {
            Connection = new SqlConnection($"Server={server};Database=master;Integrated security=true;");
            try
            {
                Connection.Open();
                //Console.WriteLine("connessione a:" + Connection.Database);

                string closeConnectionsQuery = $"ALTER DATABASE {nomeDB} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                SqlCommand cmd = new SqlCommand(closeConnectionsQuery, Connection);
                cmd.ExecuteNonQuery();
                //Statistiche(Connection,query);


                cmd = new SqlCommand(query, Connection);
                cmd.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"\nQUERY ERRATA:\n{query}");

            }
            catch (Exception e)
            {
                Console.WriteLine("Errore generico" + "\n" + e.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        private void CreaTabelle()
        {
            //query creazione tabelle
            Update("create table Account\r\n(\r\n\tid int primary key identity(1,1),\r\n\tpropic varbinary(max),\r\n\tusername varchar(50),\r\n\temail varchar(50),\r\n\tpsw varchar(200),\r\n\truolo varchar(20),\r\n\tpfedelta int\r\n);");
            Update("create table Sandali\r\n(\r\n\tid int primary key identity(1,1),\r\n\tnome varchar(50),\r\n\tmarca varchar(50),\r\n\tdescrizione varchar(1000),\r\n\tprezzo decimal,\r\n\tSKU varchar(10),\r\n\tCONSTRAINT UQ_Sandali_SKU UNIQUE (SKU),\r\n\tcategoria varchar(20),\r\n\tgenere varchar(10),\r\n\tsconto decimal,\r\n\tquantita int,\r\n\ttaglia int,\r\n\tcolore varchar(10)\r\n);");
            Update("create table Carrello\r\n(\r\n\tid int primary key identity(1,1),\r\n\tidAccount int,\r\n\tFOREIGN KEY(idAccount) REFERENCES Account(id) \r\n\tON UPDATE CASCADE ON DELETE SET NULL,\r\n\tidSandali int,\r\n\tFOREIGN KEY(idSandali) REFERENCES Sandali(id) \r\n\tON UPDATE CASCADE ON DELETE SET NULL\r\n);\r\n");
            Update("create table Wishlist\r\n(\r\n\tid int primary key identity(1,1),\r\n\tidAccount int,\r\n\tFOREIGN KEY(idAccount) REFERENCES Account(id) \r\n\tON UPDATE CASCADE ON DELETE SET NULL,\r\n\tidSandali int,\r\n\tFOREIGN KEY(idSandali) REFERENCES Sandali(id) \r\n\tON UPDATE CASCADE ON DELETE SET NULL\r\n);\r\n");
            Update("create table Immagini\r\n(\r\n\tid int primary key identity(1,1),\r\n\timmagine varbinary(max),\r\n\tSKU varchar(10),\r\n\tFOREIGN KEY(SKU) REFERENCES Sandali(SKU) \r\n\tON UPDATE CASCADE ON DELETE SET NULL,\r\n);\r\n");

            //query inserimento in tabella
        }
    }
}
