using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.weapons
{
    public abstract class Weapon
    {
        public string type { get; set; }
        public string Name { get; set; }
        public Level Level { get; set; }
        public double Dps { get; set; }
        public double damageFactor { get; set; } = 1.0;

        public double getDamageFactor()
        {
            return damageFactor;
        }

        public abstract List<string> Attack(Monster m);

        public Weapon() { }

        public abstract string WeaponInfo();

        public abstract void setDpsFromSave(int level);
    }

    
}
