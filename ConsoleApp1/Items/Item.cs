using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RPG.Items
{
    [Serializable]
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }

        public Item() 
        {
            Cost = 0;
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
