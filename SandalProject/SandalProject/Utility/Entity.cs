using System.Reflection;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace SandalProject.Utility
{
    public abstract class Entity
    {
        public int Id { get; set; }

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
                            // Check the underlying type of the nullable
                            var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                            if (underlyingType == typeof(int))
                            {
                                if (int.TryParse(riga[property.Name.ToLower()], out int intValNull))
                                {
                                    valore = (int?)intValNull;
                                }
                            }
                            else if (underlyingType == typeof(double))
                            {
                                if (double.TryParse(riga[property.Name.ToLower()], out double doubleVala))
                                {
                                    valore = doubleVala; // Or store it in a double variable
                                }
                            }
                            // Add other cases as needed for different nullable types
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
                            // Converti la stringa in un array di byte
                            var byteString = riga[property.Name.ToLower()];

                            // Assicurati che la stringa non sia nulla o vuota
                            if (!string.IsNullOrEmpty(byteString))
                            {
                                // Divide la stringa in base al carattere '-'
                                string[] byteValues = byteString.Split('-');

                                // Crea un array di byte della lunghezza necessaria
                                byte[] bytes = new byte[byteValues.Length];

                                // Converte ogni parte in byte
                                for (int i = 0; i < byteValues.Length; i++)
                                {
                                    bytes[i] = Convert.ToByte(byteValues[i], 16); // Converti da esadecimale a byte
                                }

                                valore = bytes; // Imposta il valore dell'array di byte
                                // Mostra i byte in un formato leggibile
                            }
                            break;

                        case "string":
                            valore = riga[property.Name.ToLower()];
                                break;
                    }

                    property.SetValue(this, valore);
                }
            }
        }
    }
}