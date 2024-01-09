using System.Text;


namespace NBC_TextGame
{
    internal class Item
    {
        public int id { get; set; }     //추후 딕셔너리로 변환하기 위한 PK
        public bool isEqipped { get; set; } = false;
        public string name { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int hp { get; set; }
        public string info { get; set; }
        public int price { get; set; }

        public Item(int id, bool isEqipped, string name, int atk, int def, int hp, string info, int price)
        {
            this.id = id;
            this.isEqipped = isEqipped;
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
