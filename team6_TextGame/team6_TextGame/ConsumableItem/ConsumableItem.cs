using NBC_TextGame;
using System.Text;

namespace team6_TextGame.ConsumableItem
{
    internal class ConsumableItem : Item
    {
        public string effect;
        public int count;

        public ConsumableItem(int id, string name, int atk, int def, int hp, string info, int price, string effect, int count) : base(id, name, atk, def, hp, info, price)
        {
            this.id = id;
            this.name = name;
            this.atk = atk;
            this.def = def;
            this.hp = hp;
            this.info = info;
            this.price = price;
            this.effect = effect;
            this.count = count;
        }
        public void ShowState()
        {
            if (count > 0)
                Console.WriteLine(name + " | " + info + " | " + count + "개");
            else
                return;
        }

        public void Consume(Character player)
        {
            if (atk>0)
            {
                player.atk += atk;
            }
            else if (def > 0)
            {
                player.def += def;
            }
            else if (hp > 0)
            {
                player.hp += hp;
            }
        }
    }
}
