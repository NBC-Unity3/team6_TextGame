using Newtonsoft.Json;
using team6_TextGame.Items;

namespace team6_TextGame.Characters
{
    internal class Player : Character
    {
        public string job { get; set; }
        public int gold { get; set; }
        public int exp { get; set; }

        public bool weaponEquip = false;
        public bool armorEquip = false;

        public List<EquipItem> equips = new List<EquipItem>();      //장비 아이템 리스트
        public List<ConsumeItem> consumes = new List<ConsumeItem>();
        private List<int> expToLvUp = new List<int>() { 10, 35, 65, 100 }; // 레벨업 필요 경험치

        [JsonConstructor]
        protected Player(string name = "", int atk = 0, int def = 0, int hp = 0, int mp = 0, string jop = "플레이어", int gold = 1500, int exp = 0, int level = 1)
        {
            this.name = name;
            this.level = level;
            this.f_atk = atk;
            this.atk = atk;
            this.f_def = def;
            this.def = def;
            this.maxHp = hp;
            this.hp = hp;
            this.maxMp = mp;
            this.mp = mp;
            this.job = jop;
            this.gold = gold;
            this.exp = exp;
        }

        public void ShowInfo()
        {
            //Console.WriteLine($"Lv. {floor}\n{name} ({job})\n공격력 : {f_atk} (+{f_atk - atk})\n방어력 : {f_def} (+{f_def - def})\n체력 : {f_hp} (+{f_hp - hp})\nGold : {gold} G");

            Console.WriteLine($"Lv. {level}");
            Console.WriteLine($"{name} ({job})");
            Console.Write($"공격력 : {f_atk} ");
            if (f_atk - atk != 0) UI.TextColor($"(+{f_atk - atk})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"방어력 : {f_def} ");
            if (f_def - def != 0) UI.TextColor($"(+{f_def - def})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"H    P : {hp} \n");
            //if (maxHp - hp != 0) Program.ui.TextColor($"(+{maxHp - hp})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"M    P : {mp} \n");
            //if (maxMp - mp != 0) Program.ui.TextColor($"(+{maxMp - mp})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.WriteLine($"Gold : {gold} G");
        }

        /*
        public void AddInventory(Item item)
        {
            inventory.Add(item);
        }
        */
        public bool HasItem(Item item)
        {
            return equips.Any(equipItem => equipItem.id == item.id) ||
                   consumes.Any(consumeItem => consumeItem.id == item.id);
        }

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
            maxHp += hp;
        }

        public void ChangeMP(int amount)
        {
            mp += amount;
            if(mp < 0) mp = 0;
        }

        public virtual void Skill_1(Monster monster)
        {
            //단일기
        }

        public virtual void Skill_2(List<Monster> monsters)
        {
            //광역기
        }

        public void ReceiveGold(int amount)
        {
            gold += amount;
        }

        public void ReceiveExp(int amount)
        {
            if (level > expToLvUp.Count)
            {
                // 이미 최고 레벨, 경험치 획득 불가 메시지
                return;
            }

            exp += amount;

            if (exp >= expToLvUp[level - 1])
                LvUp();
        }

        private void LvUp()
        {
            exp -= expToLvUp[level - 1];
            level++;
            // 레벨 업 UI
            if (level >= expToLvUp.Count + 1)
            {
                exp = 0;
                Console.WriteLine("축하합니다! 최대 레벨에 도달했습니다!");
            }
            else
            {
                // 레벨업 UI 메시지
                Console.WriteLine($"레벨업! 현재 레벨: {level}");
            }
        }
    }
}
