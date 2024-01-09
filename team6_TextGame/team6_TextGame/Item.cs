using System.Text;


namespace team6_TextGame
{
    internal class Item(int id, string name, int atk, int def, int hp, string info, int price)
    {
        public int id { get; set; } = id;
        public string name { get; set; } = name;
        public int atk { get; set; } = atk;
        public int def { get; set; } = def;
        public int hp { get; set; } = hp;
        public string info { get; set; } = info;
        public int price { get; set; } = price;

        public string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(name);
            sb.Append(" | ");
            sb.Append(info);

            return sb.ToString();
        }

    }
}
