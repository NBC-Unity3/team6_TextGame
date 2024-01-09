using System.Text;
using team6_TextGame;

namespace team6_TextGame
{
    internal class ConsumableItem : Item
    {
        public string effect;
        
        public ConsumableItem(int id, string name, int atk, int def, int hp, string info, int price, string effect) : base(id, name, atk, def, hp, info, price)
        {
            this.id = id;
            this.name = name;
            this.atk = atk;
            this.def = def;
            this.hp = hp;
            this.info = info;
            this.price = price;
            this.effect = effect;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(name);
            sb.Append(" | ");
            sb.Append(effect);
            sb.Append("| ");
            sb.Append(info);

            return sb.ToString();
        }
    }
}
