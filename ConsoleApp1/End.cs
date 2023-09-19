using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RPG.Items;
using System.IO;

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
    }
}
