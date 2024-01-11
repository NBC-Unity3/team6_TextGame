using team6_TextGame.Items;

namespace team6_TextGame.Characters
{
    internal class Player : Character
    {
        //자동구현 프로퍼티가 자동으로 private field를 만든다.
        public string job { get; set; }
        public int gold { get; set; } = 1500;

        //public List<Item> inventory = new List<Item>();      //장비 아이템 리스트
        static UI ui = new UI();
        public List<EquipItem> equips = new List<EquipItem>();      //장비 아이템 리스트
        public List<ConsumeItem> consumes = new List<ConsumeItem>();

        protected Player(string name = "", int level = 1, int atk = 0, int def = 0, int hp = 0, int mp = 0, string jop = "플레이어", int gold = 1500)
        {
            this.level = level;
            this.gold =
            this.f_atk = atk;
            this.atk = atk;
            this.f_def = def;
            this.def = def;
            this.maxhp = hp;
            this.hp = hp;
            this.maxmp = mp;
            this.mp = mp;
            this.job = jop;
            this.gold = gold;
        }

        public void ShowInfo()
        {
            //Console.WriteLine($"Lv. {level}\n{name} ({job})\n공격력 : {f_atk} (+{f_atk - atk})\n방어력 : {f_def} (+{f_def - def})\n체력 : {f_hp} (+{f_hp - hp})\nGold : {gold} G");

            Console.WriteLine($"Lv. {level}");
            Console.WriteLine($"{name} ({job})");
            Console.Write($"공격력 : {f_atk} ");
            if (f_atk - atk != 0) ui.TextColor($"(+{f_atk - atk})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"방어력 : {f_def} ");
            if (f_def - def != 0) ui.TextColor($"(+{f_def - def})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"H    P : {hp} \n");
            //if (maxhp - hp != 0) Program.ui.TextColor($"(+{maxhp - hp})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"M    P : {mp} \n");
            //if (maxmp - mp != 0) Program.ui.TextColor($"(+{maxmp - mp})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.WriteLine($"Gold : {gold} G");
        }

        /*
        public void AddInventory(Item item)
        {
            inventory.Add(item);
        }
        */

        public void AddEquipsInven(Item item)
        {
            equips.Add(item as EquipItem);
        }

        public void AddConsumesInven(Item item)
        {
            consumes.Add(item as ConsumeItem);
        }

        public void ChangeStatus(int atk, int def, int hp)
        {
            f_atk += atk;
            f_def += def;
            maxhp += hp;
        }

        public virtual int Skill_1(Monster monster)
        {
            //단일기
            return 0;
        }

        public virtual int Skill_2(Monster[] monster)
        {
            //광역기
            return 0;
        }
    }
}
