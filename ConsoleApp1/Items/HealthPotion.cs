using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RPG.Items
{
    [Serializable]
    public class HealthPotion : Item
    {
        public HealthPotion() 
        { 
            Name = "HealthPotion";
            Description = "Double's player health";
            Cost = 100;
        }

        public void Use(Player player)
        {
            debuffs.Debuff st = new debuffs.Debuff("Strengthen");
            player.debuffs.Add(st);
            st.AddDebuff(player);
        }

    }
}
