using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.debuffs
{
    public class Debuff
    {
        public string name { get; set; }
        public string description { get; set; }
        public int count { get; set; } = 0;
        public Debuff(string n)
        {
            name = n;
        }

        public void AddDebuff(Player p)
        {
            switch (name)
            {
                case "Weaken":
                    description = "take more dmg";
                    AddWeaken(p);
                    break;
                case "Nullify":
                    description = "do less dmg";
                    AddNullify(p);
                    break;
                case "Strengthen":
                    description = "have 2x hp";
                    AddStrengthen(p);
                    break;
            }
        }

        public void AddWeaken(Player p) 
        {
            p.DamageFactor += 0.2;
            count++;
            p.debuffs.Add(this);
        }

        public void AddNullify(Player p)
        {
            p.weapon.damageFactor += 0.2;
            count++;
            p.debuffs.Add(this);
        }

        public void AddStrengthen(Player p)
        {
            p.healthFactor *= 2;
            count++;
            p.debuffs.Add(this);
        }

    }
}
