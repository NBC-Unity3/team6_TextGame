using team6_TextGame.Monsters;

namespace team6_TextGame
{
    internal class Character
    {
        //자동구현 프로퍼티가 자동으로 private field를 만든다.
        public int level { get; set; } = 1;
        public string name { get; set; } = "";
        public string job { get; set; }
        public int atk {  get; set; }
        public int def {  get; set; }
        public int hp {  get; set; }
        public int mp {  get; set; }
        public int gold { get; set; } = 1500;

        //아이템, 스킬로 인한 최종 능력치
        public int f_atk;
        public int f_def;
        public int f_hp;
        public int f_mp;

        public List<Item> inventory = new List<Item>();      //장비 아이템 리스트

        public Character()
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

    class Warrior : Character
    {
        public Warrior()
        {
            atk = 10;
            def = 5;
            hp = 100;
            mp = 50;
            job = "전사";
            f_atk = atk;
            f_def = def;
            f_hp = hp;
            f_mp = mp;
        }

        public override int Skill_1(Monster monster)
        {
            //단일기
            int damage = atk * 2;
            mp -= 10;


            //monster.hp -= damage;
            //Console.WriteLine($"알파 스트라이크!\n{monster.name}에게 {damage}만큼의 대미지를 입혔습니다.");
            return damage;
        }

        public virtual int Skill_2(Monster[] monster)
        {
            //광역기
            int damage = (int) Math.Round(atk * 1.2);
            mp -= 15;

            //string names = "";
            //for(int i = 0; i < monster.Length; i++)
            //{
            //    monster[i].hp -= (int) damage;
            //    names += monster[i].name = " ";
            //}

            //Console.WriteLine($"더블 스트라이크!\n모두에게 {damage}만큼의 대미지를 입혔습니다.");

            return damage;
        }
    }

    class Archer : Character
    {
        public Archer()
        {
            atk = 12;
            def = 3;
            hp = 100;
            mp = 40;
            job = "궁수";
            f_atk = atk;
            f_def = def;
            f_hp = hp;
            f_mp = mp;
        }

        public override int Skill_1(Monster monster)
        {
            //단일기, 명중 부위 따라 데미지가 다른 설정
            Random rand = new Random();

            mp -= 15;

            int part = rand.Next(1, 3);

            int damage = 0;

            switch (part)
            {
                case 1: //급소 관통
                    damage = atk * 3;
                    break;
                case 2: //주요 팔다리 명중
                    damage = (int)Math.Round(atk * 1.2);
                    break;
                case 3: //빗맞음
                    damage = (int)Math.Round(atk * 0.8);
                    break;
            }
            return damage;
        }

        public virtual int Skill_2(Monster[] monster)
        {
            mp -= 5; //자주 사용 가능하게, 하지만 위력 안높음

            //광역기, 불화살로 본대 전체 타격
            int damage = atk;
            return damage;
        }
    }

    class Mage : Character
    {
        public Mage()
        {
            atk = 8;
            def = 4;
            hp = 100;
            mp = 70;
            job = "마법사";
            f_atk = atk;
            f_def = def;
            f_hp = hp;
            f_mp = mp;
        }

        public override int Skill_1(Monster monster)
        {
            //속성 단일 공격 -> 몬스터마다 다르게 설정하고 싶은데 몬스터 받아오는 형태로 여기서 공격할지
            //                  아니면 그냥 진짜 데미지값만 리턴할지 모르겠음
            int damage = atk * 2;
            mp -= 10;

            return damage;
        }

        public virtual int Skill_2(Monster[] monster)
        {
            //광역기
            int damage = (int)Math.Round(atk * 1.5);
            return damage;
        }
    }

}
