using System.Reflection;
using System.Data.SqlClient;
using System.Text;

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

                        case "system.byte[]":
                            var byteString = riga[property.Name.ToLower()];
                            valore = Convert.FromBase64String(byteString); 
                            break;
                    }

                    property.SetValue(this, valore);
                }
            }
        }

    }
}