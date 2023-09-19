using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPG.weapons
{
    class Hand : Weapon
    {
        [JsonConstructor]
        public Hand()
        {
            type = "hand";
            Name = "unnamed-hand";
            Level = new Level();
            Dps = 10.0 * (1.0+(((double)(Level.currLevel))/(100.0)));
        }

        public override List<string> Attack(Monster m)
        {
            List<string> res = new List<string>();
            m.hp -= Dps;
            Level.AddLevelToWeapon((int)Dps / 10);
            res.Add($"{m.name} has taken {Dps} damage from you!");
            res.Add($"{m.name} has {m.hp} health left!");
            if(m.hp <= 0)
            {
                m.isAlive = false;
            }
            return res;
        }

    }
}
