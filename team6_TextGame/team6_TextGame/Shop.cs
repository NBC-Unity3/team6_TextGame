using Newtonsoft.Json;


namespace team6_TextGame
{
    internal class Shop
    {
        public List<EquipmentItem> items = new List<EquipmentItem>();

        public void AddItem(EquipmentItem item)
        {
            this.items.Add(item);
        }

        public void BuyItem(int index, Character player)
        {
            if (items[index].price >  player.gold)
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
            else
            {
                player.gold -= items[index].price;
                player.AddInventory(items[index]);
                items.RemoveAt(index);
                SaveOptions();
            }
        }

        public void SellItem(int index, Character player)
        {
            EquipmentItem item = player.inven[index] as EquipmentItem;
            player.gold += (int)(item.price * 0.8);
            AddItem(item);
            player.inven.RemoveAt(index);
            SaveOptions();
        }


        public void SaveOptions()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/shop.json";
            string json = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public void LoadOptions()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/shop.json";
            if (!File.Exists(path)) SaveOptions();

            string json = File.ReadAllText(path);

            List<EquipmentItem> items = JsonConvert.DeserializeObject<List<EquipmentItem>>(json);

            this.items = items;

        }
    }
}
