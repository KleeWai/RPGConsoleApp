using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPG.monsters
{
    public class EarthMonster : Monster
    {
        public EarthMonster(int lvl)
        {
            isAlive = true;
            level = lvl;
            name = "Ram";
            type = "Earth";
            dps = level * 10;
            hp = level * 100;
        }
        public override List<string> Attack(Player p)
        {
            if (hp <= 0)
            {
                isAlive = false;
                return new List<string>();
            }
            foreach (debuffs.Debuff d in p.debuffs)
            {
                if (d.count <= 0)
                {
                    p.debuffs.Remove(d);
                }
            }
            List<string> res = new List<string>();
            res.Add($"{name} the {type} monster does {dps} damage to {p.name}");
            p.health -= dps * (p.DamageFactor);
            res.Add($"{p.name} has {p.health} health left!");
            if (p.health <= 0)
            {
                p.health = 0;
            }

            return res;
        }

        public override List<string> SpecialAttack(Player p)
        {
            if (hp <= 0)
            {
                isAlive = false;
                return new List<string>();
            }
            List<string> res = new List<string>();
            res.Add($"{name} the {type} monster has launched a SPECIAL attack on {p.name}!!!");
            res.Add($"{name} the {type} monster has applied {Constants.NullifyDebuff} {p.name}!!!");
            var weakenDebuff = new debuffs.Debuff(Constants.NullifyDebuff);
            weakenDebuff.AddWeaken(p);
            return res;
        }

        public override string LootDrop(Player p)
        {
            Random rand = new Random();
            int add = (rand.Next(this.level * 10, this.level * 20));
            p.coins += add;
            string res = ($"You have gotten {add} coins from {name}!! You now have {p.coins} coins");
            return res;
        }
    }
}
