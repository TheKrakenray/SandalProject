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
                            Console.WriteLine(riga[property.Name.ToLower()] + "Sono io");
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
                            var bytes = riga[property.Name.ToLower()];
                            Console.WriteLine(bytes);

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