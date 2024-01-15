using Newtonsoft.Json;
using System.Text;
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
            //if (maxHp - hp != 0) Program.UI.TextColor($"(+{maxHp - hp})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.Write($"M    P : {mp} \n");
            //if (maxMp - mp != 0) Program.UI.TextColor($"(+{maxMp - mp})", ConsoleColor.Yellow); else { Console.WriteLine(); }
            Console.WriteLine($"Gold : {gold} G");
        }

        public void ShowStatus()
        {
            Console.WriteLine($"[{name}]");

            var sb = new StringBuilder();
            sb.Append("Lv.");
            sb.Append(level);
            sb.Append(' ');
            sb.Append(" HP ");
            sb.Append(hp);
            sb.Append("/");
            sb.Append(maxHp);
            sb.Append(' ');
            sb.Append(" MP ");
            sb.Append(mp);
            sb.Append("/");
            sb.Append(maxMp);

            Console.WriteLine(sb.ToString());
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

        public void AddConsumesInven(ConsumeItem item)
        {
            var thisItem = consumes.Find(consumeItem => consumeItem.id == item.id);
            if (thisItem != null)
            {
                thisItem.count += item.count;
            }
            else
            {
                consumes.Add(item);
            }
        }

        public void UseItem(ConsumeItem item)
        {
            if (item.id == 9)
            {
                f_atk += 3;
                item.count--;
                UI.WriteLine("전사의 영약을 사용했습니다.\n공격력이 3 상승했습니다.");
            }
            else if (item.id == 8)
            {
                f_def += 3;
                item.count--;
                UI.WriteLine("수호자의 영약을 사용했습니다.\n방어력이 3 상승했습니다.");
            }
            else if (item.id == 7)
            {
                hp += 50;
                item.count--;
                if (hp > 100)
                    hp = 100;
                UI.WriteLine("붉은 물약을 사용했습니다.\nHp가 50 상승했습니다.");
            }
            if (item.count == 0)
            {
                consumes.Remove(item);
            }
        }

        public void ChangeStatus(int atk, int def, int hp)
        {
            f_atk += atk;
            f_def += def;
            maxHp += hp;
        }

        public bool ChangeMP(int amount)
        {
            if(mp + amount < 0)
            {
                UI.TextColor("마나가 부족합니다.", ConsoleColor.Red);
                return false;
            } else
            {
                Console.Write($"MP : {mp} → ");
                mp += amount;
                if (mp < 0) mp = 0;
                Console.WriteLine(mp);
                return true;
            }
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

        public void ReceiveItem(List<Item> amount)
        {
            foreach (Item item in amount)
            {
                if (item is EquipItem equipItem)
                {
                    equips.Add(equipItem);
                }
                else if (item is ConsumeItem consumeItem)
                {
                    AddConsumesInven(consumeItem);
                }
            }
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
