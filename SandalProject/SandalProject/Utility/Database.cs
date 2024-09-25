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
                instance = new Database("Libreria_Diego_Scampia");

            return instance;
        }


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
                    riga.Add(dr.GetName(i).ToLower(), dr.GetValue(i).ToString());

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
            Update("create table Utenti ( id int primary key identity (1,1), Username varchar (100), constraint AK_USERNAME UNIQUE(Username), Email varchar (100), constraint AK_EMAIL UNIQUE(Email), Passw varchar (100), Ruolo varchar (100) );");
            Update("create table Libro\r\n(\r\n\tid int primary key identity (1,1),\r\n\tTitolo varchar (100),\r\n\tAutore varchar (100),\r\n\tGenere varchar (100),\r\n\tDescrizione varchar (500),\r\n\tPrezzo decimal (5,2),\r\n\tCopertina varchar (200),\r\n\tQuantita int \r\n);");
            Update("create table Carrello\r\n(\r\n\tUtentiid int,\r\n\tforeign key (Utentiid) references Utenti(id)\r\n\ton update cascade on delete set null,\r\n\tLibroid int,\r\n\tforeign key (Libroid) references Libro(id)\r\n\ton update cascade on delete set null,\r\n\tDataOrdine DATETIME\r\n);");

            //query inserimento in tabella
            Update("insert into Utenti (Username, Email, Passw, Ruolo) Values ('DiegoAdmin', 'diegoprova@email.com', HASHBYTES('SHA2_512','diego123'), 'admin')");
            Update("insert into Libro " +
                "(Titolo, Autore, Genere, Descrizione, Prezzo, Copertina, Quantita) Values " +
                "('Libro rosso', 'Mario Rossi', 'Romantico', 'Libro molto bello', 21.50, 'MOCKUP-POESIA-ROSSO.jpg', 30), " +
                "('Libro blu', 'J.Kenn.', 'Fantasy', 'Libro molto molto bello', 25, 'MOCKUP-POESIA-BLU.jpg', 24), " +
                "('Libro verde', 'Mario Ciro', 'Fantasy-Thriller', 'Libro molto brutto bello', 30, 'MOCKUP-POESIA-VERDE.jpg', 10), " +
                "('Libro giallo', 'Antonio L.', 'Horror', 'Libro molto ', 26.89, 'MOCKUP-POESIA-GIALLO.jpg', 15); ");
        }
    }
}
