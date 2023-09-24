using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class Level
    {
        public int currLevel { get; set; }
        int fullRequiredXp { get; set; }
        public int currXp { get; set; }


        public Level()
        {
            currLevel = 1;
            fullRequiredXp = 100;
            currXp = 0;
        }

        public void AddLevelToPlayer(int xp)
        {
            if(xp <= fullRequiredXp)
            {
                currXp += xp;
                if(currXp > fullRequiredXp)
                {
                    currLevel++;
                    currXp = currXp - fullRequiredXp;
                    fullRequiredXp = currLevel * 100;
                }
            }
            else
            {
                int tempcurrxp = currXp;
                int tempfull = fullRequiredXp;
                currXp = 0;
                currLevel++;
                fullRequiredXp = currLevel * 100;
                AddLevelToPlayer(xp - (tempfull - tempcurrxp));
            }
        }

        public void AddLevelToWeapon(int xp)
        {
            if (xp <= fullRequiredXp)
            {
                currXp += xp;
                if (currXp > fullRequiredXp)
                {
                    currLevel++;
                    currXp = currXp - fullRequiredXp;
                    fullRequiredXp = currLevel * 100;
                }
            }
            else
            {
                int tempcurrxp = currXp;
                int tempfull = fullRequiredXp;
                currXp = 0;
                currLevel++;
                fullRequiredXp = currLevel * 100;
                AddLevelToWeapon(xp - (tempfull - tempcurrxp));
            }
        }

        public string StringPlayerLevelAndXp()
        {
            string res;
            res = ($"Level = {currLevel}, {currXp}/{fullRequiredXp}");
            return res;
        }

        public string StringWeaponLevelAndXp()
        {
            string res;
            res = ($"Level = {currLevel}, {currXp}/{fullRequiredXp}");
            return res;
        }

        public override string ToString()
        {
            return currLevel.ToString();
        }

        public void SetLevel(int level)
        {
            currLevel = level;
            currXp = 0;
            fullRequiredXp = level* 100;
        }
    }

   
}
