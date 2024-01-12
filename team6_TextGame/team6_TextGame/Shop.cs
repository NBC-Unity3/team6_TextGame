using Newtonsoft.Json;
using System.Numerics;
using team6_TextGame.Characters;
using team6_TextGame.Items;


namespace team6_TextGame
{
    internal class Shop
    {
        public List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            this.items.Add(item);
        }

        public void BuyItem(int index, Player player)
        {
            if (items[index].price > player.gold)
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
            else
            {
                player.gold -= items[index].price;
                player.AddEquipsInven(items[index]);

                //items.RemoveAt(index);
            }
        }

        public void SellItem(int index, Player player)
        {
            Item item = player.equips[index];
            player.gold += (int)(item.price * 0.8);
            //AddItem(item);
            player.equips.RemoveAt(index);
        }


        public void SaveOptions()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            string path = Path.Combine(Directory.GetCurrentDirectory(), "shop.json");
            string json = JsonConvert.SerializeObject(items, settings);
            File.WriteAllText(path, json);
        }

        public void LoadOptions()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            string path = Path.Combine(Directory.GetCurrentDirectory(), "shop.json");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<Item> loadedItems = JsonConvert.DeserializeObject<List<Item>>(json, settings);

                if (loadedItems != null)
                {
                    items = loadedItems;

                }
            }
        }
    }
}
