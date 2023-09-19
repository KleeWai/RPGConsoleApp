using RPG.monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Environment
    {
        Player player;
        bool inProgress = false;
        public Environment(Player p) 
        {
            player = p;
        }
        public void StartGame()
        {
            Random random = new Random();
            inProgress = true;

            string input = Console.ReadLine();
            while(!input.Equals("q")) 
            {
                if (input != null)
                {
                    input = input.ToLower();
                    if (input.Equals("mawoh"))
                    {
                        Console.WriteLine($"{player.name} has gone into battle!");
                        SpawnMonster();
                    }
                }
                inProgress = false;
                input = Console.ReadLine();
            }
            if(input.Equals("q"))
            {
                End e = new End(player);
                e.SaveInventory(player.Inventory);
                System.Environment.Exit(0);
            }
        }

        public void SpawnMonster()
        {
            Random rand = new Random();
            int randlvl = rand.Next(1, player.returnLevel()+3);
            int randMonster = rand.Next(0, 100);
            Monster monster;
            if(randMonster < 33)
            {
                monster = new IceMonster(randlvl);
            }
            else if(randMonster <= 66)
            {
                monster = new FireMonster(randlvl);
            }
            else
            {
                monster = new EarthMonster(randlvl);
            }
            Console.WriteLine($"A level {monster.level} {monster.name} with {monster.hp}hp has spawned!");
            int turn = 1;
            while(player.health > 0 && monster.isAlive)
            {
                Console.WriteLine($"Turn: {turn}");
                List<string> monsterAttacks = monster.Attack(player);
                foreach(string attack in monsterAttacks)
                {
                    Console.WriteLine($"{attack}");
                }
                if(player.health <= 0)
                {
                    Console.WriteLine($"{player.name} has died!");
                }
                List<string> playerAttacks = player.weapon.Attack(monster);
                foreach (string attack in playerAttacks)
                {
                    Console.WriteLine($"{attack}");
                }
                turn++;
            }
            if(!monster.isAlive)
            {
                Console.WriteLine(monster.LootDrop(player));
            }

            inProgress = false;
            player.health = player.maximumHealth;
            StartGame();
        }
    }
}
