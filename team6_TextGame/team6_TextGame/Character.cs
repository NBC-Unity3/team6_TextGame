using team6_TextGame;
using team6_TextGame.Items;
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

        public List<EquipItem> equips = new List<EquipItem>();      //장비 아이템 리스트
        public List<ConsumeItem> consumes = new List<ConsumeItem>();

        static UI ui;

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
            if (f_atk - atk != 0) ui.TextColor($"(+{f_atk - atk})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"방어력 : {f_def} ");
            if (f_def - def != 0) ui.TextColor($"(+{f_def - def})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"H    P : {f_hp} ");
            if (f_hp - hp != 0) ui.TextColor($"(+{f_hp - hp})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"M    P : {f_mp} ");
            if (f_mp - mp != 0) ui.TextColor($"(+{f_mp - mp})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.WriteLine($"Gold : {gold} G");
        }

        public void AddInventory(Item item)
        {
            equips.Add(item);

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
            int damage = f_atk * 2;
            mp -= 10;


            //monster.hp -= damage;
            //Console.WriteLine($"알파 스트라이크!\n{monster.name}에게 {damage}만큼의 대미지를 입혔습니다.");
            return damage;
        }

        public virtual int Skill_2(Monster[] monster)
        {
            //몬스터 무리 중 2마리 랜덤 공격
            int damage = (int) Math.Round(f_atk * 1.2);
            mp -= 15;

            /* 공격 여기서 실행할 경우
            if(monster.Length < 2) //남은 몬스터가 1마리일때
            {
                monster[0].hp -= damage;
                Console.WriteLine($"{monster[0].name}에게 {damage}의 데미지를 입혔습니다.");
            } else
            {
                int first, second;
                Random rand = new Random();
                first = rand.Next(0, monster.Length - 1);
                while(true)
                {
                    int b = rand.Next(0, monster.Length - 1);
                    if (first == b) continue;
                    else
                    {
                        second = b;
                        break;
                    }
                }
            }
            */

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
            int damage = 0;
            mp -= 15;

            Random rand = new Random();
            int part = rand.Next(1, 3);
            switch (part)
            {
                case 1: //급소 관통
                    damage = f_atk * 3;
                    break;
                case 2: //주요 팔다리 명중
                    damage = (int)Math.Round(f_atk * 1.2);
                    break;
                case 3: //빗맞음
                    damage = (int)Math.Round(f_atk * 0.8);
                    break;
            }

            //monster.hp -= damage;
            return damage;
        }

        public virtual int Skill_2(Monster[] monster)
        {
            //광역기, 불화살로 본대 전체 타격
            int damage = f_atk;
            mp -= 5; //자주 사용 가능하게, 하지만 위력 안높음

            /* 공격 구현
            foreach(Monster m in monster)
            {
                m.hp -= f_atk;
            }

            Console.WriteLine($"적진에 불이 붙었습니다.\n모든 몬스터가 {damage}의 데미지를 받습니다.");
            */
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
            int damage = f_atk;
            mp -= 10;

            //속성 단일 공격 -> 몬스터마다 다르게 설정해봄 공허충에 강한 설정
            /*
            switch (monster.name)
            {
                case "미니언":
                    damage = f_atk;
                    break;
                case "대포 미니언":
                    damage = (int)Math.Round(f_atk * 0.8);
                    break;
                case "공허충":
                    damage = (int)Math.Round(f_atk * 1.2);
                    break;
            }
            */
            return damage;
        }

        public virtual int Skill_2(Monster[] monster)
        {
            //전체 광역기
            int damage = (int)Math.Round(f_atk * 1.5);
            mp -= 20;
            /*
            for(int i = 0; i < monster.Length; i++)
            {
                monster[i].hp -= damage;
            } */

            return damage;
        }
    }
}
