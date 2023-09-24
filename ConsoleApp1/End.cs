using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RPG.Items;
using System.IO;
using System.Reflection;

namespace RPG
{
    public class End
    {
        Player pl;
        public End(Player p)
        {
            pl = p;
        }

        public void SaveInventory(List<Item> items)
        {
            const string FilePath = "items.json";
            string json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public void SaveAll() 
        {
            pl.currXp = pl.level.currXp;
            const string FILE_NAME = Constants.SaveFileName;
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.OpenOrCreate))
            {
                Type type = pl.GetType();
                PropertyInfo[] properties = type.GetProperties();
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    // Write the data to the file
                    foreach (PropertyInfo property in properties)
                    {
                        object value = property.GetValue(pl);
                        writer.WriteLine($"{property.Name}:{value.ToString()}");
                        // You can also access and manipulate the property value here if needed
                    }
                    type = pl.weapon.GetType();
                    properties = type.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        object value = property.GetValue(pl.weapon);
                        writer.WriteLine($"{property.Name}:{value.ToString()}");
                        // You can also access and manipulate the property value here if needed
                    }

                } // 

            }
        }
    }
}
