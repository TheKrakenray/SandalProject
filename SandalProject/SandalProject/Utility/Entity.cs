using System.Reflection;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace SandalProject.Utility
{
    public abstract class Entity
    {
        public int? Id { get; set; }

        public Entity()
        {

        }

        public Entity(int id)
        {
            Id = id;
        }

        public virtual string ToString()
        {
            string ris = "";

            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                if (property?.Name == null)
                    ris += "";
                else if (property?.GetValue(this) == null)
                    ris += $"{property?.Name}: NULL\n";
                else
                    ris += $"{property?.Name}: {property?.GetValue(this)}\n";
            }

            return ris;
        }

        public virtual string ToString(string name, string replace)
        {
            string ris = "";

            foreach (PropertyInfo property in this.GetType().GetProperties())
            {

                if (property?.Name == null)
                    ris += "";
                else if (property?.GetValue(this) == null)
                    ris += $"{property?.Name}: NULL\n";
                else
                {
                    if (property?.Name == name)
                        ris += replace;
                    else
                        ris += $"{property?.Name}: {property?.GetValue(this)}\n";
                }
            }

            return ris;
        }

        public void FromDictionary(Dictionary<string, string> riga)
        {
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                if (riga.ContainsKey(property.Name.ToLower()))
                {
                    Console.WriteLine(property.PropertyType.Name.ToLower());

                    object? valore = null;

                    switch (property.PropertyType.Name.ToLower())
                    {
                        case "int32":
                            if (int.TryParse(riga[property.Name.ToLower()], out int intVal))
                            {
                                valore = (int?)intVal;
                            }
                            break;

                        case "nullable`1":
                            if (int.TryParse(riga[property.Name.ToLower()], out int intValNull))
                            {
                                valore = (int?)intValNull;
                            }
                            break;

                        case "double":
                            string doubleString = riga[property.Name.ToLower()].Replace(",", ".");
                            if (double.TryParse(doubleString, out double doubleVal))
                            {
                                valore = (double?)doubleVal; 
                            }
                            break;

                        case "bool":
                            if (bool.TryParse(riga[property.Name.ToLower()], out bool boolVal))
                            {
                                valore = boolVal;
                            }
                            break;

                        case "datetime":
                            if (DateTime.TryParse(riga[property.Name.ToLower()], out DateTime dateVal))
                            {
                                valore = (DateTime?)dateVal; 
                            }
                            break;

                        case "byte[]":
                            var val = riga[property.Name.ToLower()];
                            byte[] b = File.ReadAllBytes(val);
                            valore = "data:image/png;base64," + Convert.ToBase64String(b);
                            var byteString = riga[property.Name.ToLower()];

                            Console.WriteLine($"byteString: {byteString}"); // Stampa il contenuto di byteString

                            try
                            {
                                valore = Convert.FromBase64String(byteString);
                                Console.WriteLine($"Converted from Base64: {BitConverter.ToString((byte[])valore)}");
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine(e);
                                valore = System.Text.Encoding.UTF8.GetBytes(byteString);
                                Console.WriteLine($"Converted from UTF-8: {BitConverter.ToString((byte[])valore)}");
                            }
                            break;
                            //var byteString = riga[property.Name.ToLower()];

                            //// Verifica se la stringa è già in Base64
                            //try
                            //{
                            //    // Prova a convertire da Base64 a byte[]
                            //    valore = Convert.FromBase64String(byteString);
                            //    Console.WriteLine($"Converted from Base64: {BitConverter.ToString((byte[])valore)}");
                            //}
                            //catch (FormatException e)
                            //{
                            //    Console.WriteLine(e);
                            //    // Se la conversione fallisce, i dati non sono in Base64, considera una gestione diversa o un fallback
                            //    Console.WriteLine("I dati non sono in formato Base64 e non possono essere convertiti.");
                            //
                        case "string":
                                valore = riga[property.Name.ToLower()];
                                break;
                           
                    }
                    Console.WriteLine(valore);
                    property.SetValue(this, valore);
                }
            }
        }
    }
}