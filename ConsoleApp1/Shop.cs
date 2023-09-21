using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Items;
namespace RPG
{
    public class Shop
    {
        Player player;
        public List<Item> Items;
        public Shop(Player p) 
        {
            player = p;
            Items = new List<Item>(1)
            {
                new HealthPotion()
            };
        }

        public string printShop()
        {
            string res = string.Empty;
            int count = 1;
            foreach(Item item in Items)
            {
                res += ($"{count}: {item.Name} - {item.Cost} coins\n");
                count++;
            }
            return res;
        }

        public string purchase(int id)
        {
            if (id > 6)
            {
                return "This is not a valid id";
            }
            else if (player.coins < Items[id - 1].Cost)
            {
                return $"You don't have eneough coins!  {player.coins}/{Items[id - 1].Cost}";
            }
            else
            {
                player.coins -= Items[id - 1].Cost;
                player.AddItem(Items[id - 1]);
                return $"Successfully purchased {Items[id - 1].Name}, you now have {player.coins} coins";
            }
        }


    }
}
