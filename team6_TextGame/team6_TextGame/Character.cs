

namespace NBC_TextGame
{
    internal class Character
    {
        //자동구현 프로퍼티가 자동으로 private field를 만든다.
        public int level { get; set; } = 1;
        public string name { get; set; } = "Chad";
        public string job {  get; set; } = "전사";
        public int atk {  get; set; } = 10;
        public int def {  get; set; } = 5;
        public int hp {  get; set; } = 100;
        public int gold { get; set; } = 1500;

        //아이템, 스킬로 인한 최종 능력치
        public int f_atk;
        public int f_def;
        public int f_hp;

        public List<Item> inven = new List<Item>();

        
        public Character()
        {
            f_atk = atk;
            f_def = def;
            f_hp = hp;
        }
        
        
        public void ShowInfo()
        {
            Console.WriteLine($"Lv. {level}\n{name} ({job})\n공격력 : {f_atk} (+{f_atk - atk})\n방어력 : {f_def} (+{f_def - def})\n체력 : {f_hp} (+{f_hp - hp})\nGold : {gold} G");
        }

        public void AddInventory(Item item)
        {
            inven.Add( item );
        }

        public void ChangeStatus(int atk, int def, int hp)
        {
            f_atk += atk;
            f_def += def;
            f_hp += hp;
        }
    }
}
