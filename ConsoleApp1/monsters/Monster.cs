using RPG.debuffs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public abstract class Monster
    {
        public double hp { get; set; }
        protected int dps;
        public string name;
        protected string type;
        public int level;
        public bool isAlive;
        public List<Debuff> debuffs { get; set; }

        public abstract List<string> Attack(Player p);
        public abstract List<string> SpecialAttack(Player p);

        public abstract string LootDrop(Player p);

    }
}
