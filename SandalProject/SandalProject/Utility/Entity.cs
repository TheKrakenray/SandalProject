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
                if (riga.ContainsKey(property?.Name.ToLower()))
                {
                    object? valore = riga[property?.Name.ToLower()];

                    switch (property?.PropertyType.Name.ToLower())
                    {
                        case "int32":
                            valore = int.TryParse(riga[property.Name.ToLower()], out _) ? int.Parse(riga[property.Name.ToLower()]) : null;
                            break;

                        case "double":
                            riga[property.Name.ToLower()].Replace(",", ".");
                            valore = double.TryParse(riga[property.Name.ToLower()], out _) ? double.Parse(riga[property.Name.ToLower()]) : null;
                            break;

                        case "bool":
                            valore = bool.TryParse(riga[property.Name.ToLower()], out _) ? bool.Parse(riga[property.Name.ToLower()]) : null;
                            break;
                        case "datetime":
                            valore = DateTime.TryParse(riga[property.Name.ToLower()], out _) ? DateTime.Parse(riga[property.Name.ToLower()]) : null;
                            break;
                        case "system.byte[]":
                            valore = (byte[])valore;
                            break;
                    }

                    property.SetValue(this, valore);
                }
            }
        }
    }
}
