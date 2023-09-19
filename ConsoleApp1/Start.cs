using Newtonsoft.Json;
using RPG.Items;
using RPG.jsonstuff;
using RPG.weapons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection;
using Newtonsoft.Json;

namespace RPG
{

    class Start
    {
        private const string FILE_NAME = Constants.SaveFileName;
        public Start(Player p) 
        {
            if (File.Exists(FILE_NAME))
            {
                
                //p = DeserializePlayerFromJsonFile();
                Console.WriteLine($"{FILE_NAME} already exists! Using save file to populate data!");
                string[] lines = File.ReadAllLines(FILE_NAME);
                int breakline = 0;
                foreach (string line in lines)
                {
                    // Split the line into property name and value
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string propertyName = parts[0].Trim();
                        string propertyValue = parts[1].Trim();
                        // Use reflection to find the property by name
                        var property = p.GetType().GetProperty(propertyName);
                        if (property != null)
                        {
                            // Convert the property value to the appropriate type
                            if(property.Name == "level")
                            {
                                Level l = new Level();
                                l.SetLevel(Int32.Parse(propertyValue));
                                p.level = l;
                            }
                            else if (property.Name == "name")
                            {
                                breakline++;
                                continue;
                            }
                            else if(property.Name == "debuffs")
                            {
                                breakline++;
                                continue;
                            }
                            else if(property.Name == "weapon")
                            {
                                if (propertyName.Equals("RPG.weapons.Hand"))
                                    p.weapon = new Hand();
                                breakline++;
                                break;
                            }
                            else
                            {

                                object parsedValue = Convert.ChangeType(propertyValue, property.PropertyType);

                                // Set the property value
                                property.SetValue(p, parsedValue);
                            }                          
                        }
                    }
                    breakline++;
                }
                for (int i = breakline+1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(':');
                    string propertyName = parts[0].Trim();
                    string propertyValue = parts[1].Trim();
                    var property = p.weapon.GetType().GetProperty(propertyName);
                    if (parts.Length == 2)
                    {
                        if (property.Name == "type")
                        {
                            p.weapon.type = propertyValue.ToString();
                        }
                        if (property.Name == "Name")
                        {
                            p.weapon.Name = propertyValue.ToString();
                        }
                        if (property.Name == "Level")
                        {
                            Level l = new Level();
                            l.SetLevel(Int32.Parse(propertyValue));
                            p.weapon.Level = l;
                        }
                    }
                }
                p.Inventory = LoadItems();
                return;
            }

            
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.CreateNew))
            {
                Type type = p.GetType();
                PropertyInfo[] properties = type.GetProperties();
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    // Write the data to the file
                    foreach (PropertyInfo property in properties)
                    {
                        object value = property.GetValue(p);
                        writer.WriteLine($"{property.Name}:{value.ToString()}");
                        // You can also access and manipulate the property value here if needed
                    }
                    type = p.weapon.GetType();
                    properties = type.GetProperties();
                    foreach(PropertyInfo property in properties)
                    {
                        object value = property.GetValue(p.weapon);
                        writer.WriteLine($"{property.Name}:{value.ToString()}");
                        // You can also access and manipulate the property value here if needed
                    }

                } // 
               
            }
        }

        //inventory stuff
        public List<Item> LoadItems()
        {
            const string FilePath = "items.json";
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Item>>(json);
            }
            else
            {
                return new List<Item>(); // Return an empty list if the file doesn't exist.
            }
        }
    }
}
