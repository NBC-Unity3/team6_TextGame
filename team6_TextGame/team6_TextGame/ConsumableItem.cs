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
    }
}
