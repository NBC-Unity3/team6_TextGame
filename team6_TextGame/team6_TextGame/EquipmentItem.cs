using System.Text;
using team6_TextGame;


namespace team6_TextGame
{
    internal class EquipmentItem : Item
    {
        public bool isEqipped { get; set; } = false;


        public EquipmentItem(int id, string name, int atk, int def, int hp, string info, int price) : base(id, name, atk, def, hp, info, price)
        {
            this.id = id;
            this.name = name;
            this.atk = atk;
            this.def = def;
            this.hp = hp;
            this.info = info;
            this.price = price;
        }

        public string ToString()
        {
            var sb = new StringBuilder();
            if(isEqipped) { sb.Append("[E]"); }
            sb.Append(name);
            sb.Append(" | ");
            if(atk != 0) { sb.Append("공격력 +" +  atk + " "); }
            if(def != 0) { sb.Append("방어력 +" + def + " "); }
            if(hp != 0) { sb.Append("체력 +" + hp + " "); }
            sb.Append("| ");
            sb.Append(info);

            return sb.ToString();
        }

        public void eqip(Character player)
        {
            if (isEqipped)
            {
                isEqipped = false;
                player.ChangeStatus(-atk, -def, -hp);
            }
            else
            {
                isEqipped = true;
                player.ChangeStatus(atk, def, hp);
            }
        }
    }
}
