using System.Text;


namespace team6_TextGame
{
    internal abstract class Item()
    {
        public int id { get; set; }
        public string name { get; set; }
        public int atk { get; set; } 
        public int def { get; set; } 
        public int hp { get; set; }
        public string info { get; set; }
        public int price { get; set; }

        public abstract string ToString();
    }
}
