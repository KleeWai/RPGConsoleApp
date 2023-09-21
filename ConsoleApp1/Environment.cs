using RPG.monsters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Environment
    {
        Player player;
        bool inProgress = false;
        bool hasHuntedRecently = false;
        Stopwatch huntTimer = new Stopwatch();
        public Environment(Player p) 
        {
            player = p;
        }
        public void StartGame()
        {
            Random random = new Random();
            inProgress = true;

            string input = Console.ReadLine();
            
            while (!input.Equals("q")) 
            {
                if (input != null)
                {           
                    input = input.ToLower();
                    if (input.Equals("mawoh"))
                    {
                        if(huntTimer.IsRunning &&  huntTimer.Elapsed.TotalSeconds < 3 )
                        {
                            Console.WriteLine($"YOU HAVE HUNTED RECENTLY! Please hunt again in {3- huntTimer.Elapsed.TotalSeconds} seconds");
                            StartGame();
                        }
                        else
                        {
                            huntTimer.Restart();
                            Console.WriteLine($"{player.name} has gone into battle!");
                            SpawnMonster();
                        }               
                    }
                    if(input.Equals("mawolvl"))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"{player.level.StringPlayerLevelAndXp()}");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (input.Equals("mawostats"))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"You have killed {player.MonstersKilled} monsters!");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (input.Equals("mawohelp"))
                    {
                        Console.WriteLine(Constants.mawoHelp);
                    }
                    if (input.Equals("mawoshop"))
                    {
                        Shop s = new Shop(player);
                        Console.WriteLine(s.printShop());
                        Console.WriteLine($"Enter an id number to purchase. You have {player.coins} coins. (Type anything else to quit)");
                        string purchaseID = Console.ReadLine();
                        try
                        {
                            int parsed = Int32.Parse(purchaseID);
                            Console.WriteLine(s.purchase(parsed));
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Unable to buy");
                        }
                    }
                    if (input.Equals("secret"))
                    {
                        Constants.SecretSong();
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
            int randlvl = rand.Next(player.returnLevel() - 3, player.returnLevel()+3);
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{player.name} has died! You have gotten nothing from {monster.name}");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
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
                player.MonstersKilled++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(monster.LootDrop(player));
                Console.ForegroundColor = ConsoleColor.White;
            }

            inProgress = false;
            player.health = player.maximumHealth;
            StartGame();
        }
    }
}
