using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using RPG.debuffs;
using RPG.weapons;
using RPG.jsonstuff;
using RPG.Items;

namespace RPG
{
    public class Player
    {
        //bool isNew { get; set; } = false;
        public double health { get; set; }
        public double maximumHealth { get; set; }
        public double healthFactor { get; set; } = 1.0;
        public Level level { get; set; } = new Level();
        public int currXp { get; set; } = 0; // THIS SHOULD ONLY BE USED FOR SAVING
        public string name { get; }
        public int coins { get; set; }
        public List<Debuff> debuffs { get; set; }
        public double DamageFactor { get; set; } = 1.0;//Damage taken factor
        public int MonstersKilled { get; set; } = 0;

        [JsonConverter(typeof(WeaponConverter))]
        public Weapon weapon { get; set; }

        public List<Item> Inventory { get; set; }
        public Player(string name)
        {
            this.name = name;
            debuffs = new List<Debuff>();
            Inventory = new List<Items.Item>();
            maximumHealth = level.currLevel * 100.0 * healthFactor;
            health = maximumHealth;
            weapon = new Hand();
        }

        public int returnLevel()
        {
            return level.currLevel;
        }

        public void Addxp(int xp)
        {
            level.AddLevelToPlayer(xp);
        }

        public string StringInventory()
        {
            string res = string.Empty;
            int count = 0;
            foreach(var item in Inventory)
            {
                res += $" id: {count}, Name: {item.Name}, Count: {item.Count}\n";
                count++;
            }
            return res;
        }

        public void AddItem(Item item)
        {
            if(item is Item)
            {
                foreach(Item i in Inventory)
                {
                    if(i.Name.Equals(item.Name))
                    {
                        i.Count++;
                        return;
                    }
                }
                Inventory.Add(item);
            }
        }
    }
}
