using RPG.Items;
using System;
using System.IO;

namespace RPG
{
    class Program
    {
        
        static void Main(string[] args)
        {

            Console.WriteLine("Hello! Welcome to mawo");
            Console.WriteLine("");
            Console.WriteLine("Enter your player name");
            string n = Console.ReadLine();
            Player p = new Player(n);
            Start s = new Start(p);
            Console.WriteLine("Type mawohelp for list of available commands");
            Environment e = new Environment(p);

            e.StartGame();

        }
    }
}
