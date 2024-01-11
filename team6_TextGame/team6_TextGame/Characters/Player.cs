﻿using team6_TextGame.Items;

namespace team6_TextGame.Characters
{
    internal class Player
    {
        //자동구현 프로퍼티가 자동으로 private field를 만든다.
        public int level { get; set; } = 1;
        public string name { get; set; } = "";
        public string job { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int hp { get; set; }
        public int mp { get; set; }
        public int gold { get; set; } = 1500;

        //아이템, 스킬로 인한 최종 능력치
        public int f_atk;
        public int f_def;
        public int f_hp;
        public int f_mp;

        public List<Item> inventory = new List<Item>();      //장비 아이템 리스트

        public Player()
        {
            f_atk = atk;
            f_def = def;
            f_hp = hp;
            f_mp = mp;
        }

        public void ShowInfo()
        {
            //Console.WriteLine($"Lv. {level}\n{name} ({job})\n공격력 : {f_atk} (+{f_atk - atk})\n방어력 : {f_def} (+{f_def - def})\n체력 : {f_hp} (+{f_hp - hp})\nGold : {gold} G");

            Console.WriteLine($"Lv. {level}");
            Console.WriteLine($"{name} ({job})");
            Console.Write($"공격력 : {f_atk} ");
            if (f_atk - atk != 0) Program.TextColor($"(+{f_atk - atk})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"방어력 : {f_def} ");
            if (f_def - def != 0) Program.TextColor($"(+{f_def - def})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"H    P : {f_hp} ");
            if (f_hp - hp != 0) Program.TextColor($"(+{f_hp - hp})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"M    P : {f_mp} ");
            if (f_mp - mp != 0) Program.TextColor($"(+{f_mp - mp})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.WriteLine($"Gold : {gold} G");
        }

        public void AddInventory(Item item)
        {
            inventory.Add(item);

        }

        public void ChangeStatus(int atk, int def, int hp)
        {
            f_atk += atk;
            f_def += def;
            f_hp += hp;
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
