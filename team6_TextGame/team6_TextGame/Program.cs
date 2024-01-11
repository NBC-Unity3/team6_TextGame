﻿using team6_TextGame;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Numerics;
using team6_TextGame.Items;


class Program
{
    static QuestBoard questboard = new QuestBoard();
    static Shop shop = new Shop();
    static Character player;
    static Dungeon dungeon;

    static void Main(String[] args)
    {
        LoadGame();
        dungeon = new Dungeon(player);
        StartGame();
    }

    static Character CreateCharacter()
    {
        string name;
        Character character;
        //이름 입력
        while(true)
        {
            Console.Clear();
            Console.WriteLine("당신의 이름은 무엇입니까?");
            name = Console.ReadLine();
            Console.WriteLine("\n'{0}' 이 당신의 이름이 맞습니까?\n", name);
            Console.WriteLine("1. 맞습니다.");
            Console.WriteLine("2. 아닙니다.\n");

            var key = Console.ReadKey(true).Key;
            if (key ==  ConsoleKey.D1 || key == ConsoleKey.NumPad1)
            {
                break;
            }
            else
            {
                continue;
            }
        }
        //직업 선택

        Console.Clear();
        Console.WriteLine("원하는 직업을 선택해주세요. ");

        Console.WriteLine("======================================================");
        Console.WriteLine("         |   1. 전사    |   2. 궁수    |  3. 마법사  ");
        Console.WriteLine("---------|--------------|--------------|--------------");
        Console.WriteLine(" 공격력  |      10      |      12      |      8  ");
        Console.WriteLine("---------|--------------|--------------|--------------");
        Console.WriteLine(" 방어력  |      5       |      3       |      5  ");
        Console.WriteLine("---------|--------------|--------------|--------------");
        Console.WriteLine("  H   P  |     100      |     100      |      80  ");
        Console.WriteLine("---------|--------------|--------------|--------------");
        Console.WriteLine("  M   P  |      50      |      40      |      70  ");
        Console.WriteLine("======================================================");

        bool isFirst = true;
        while (true)
        {
            if(!isFirst)
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
            }

            isFirst = false;

            var key = Console.ReadKey(true).Key;

            if(key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
            {
                character = new Warrior();
                break;
            } else if(key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
            {
                character = new Archer();
                break;
            } else if(key == ConsoleKey.D3 || key == ConsoleKey.NumPad3)
            {
                character = new Mage();
                break;
            } else
            {
                continue;
            }
        }

        character.name = name;
        return character;
    }

    static void StartGame()
    {
        while (true) {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine($"1. 상태 보기\n2. 전투 시작 (현재 진행 : {dungeon.level}층)\n3. 인벤토리\n4. 상점\n5. 퀘스트\n6. 저장하기\n\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey n when(n==ConsoleKey.D1 || n==ConsoleKey.NumPad1):
                    Status();
                    break;
                case ConsoleKey n when (n == ConsoleKey.D2 || n == ConsoleKey.NumPad2):
                    dungeon.StartBattle();
                    break;
                case ConsoleKey n when (n == ConsoleKey.D3 || n == ConsoleKey.NumPad3):
                    Inventory();
                    break;
                case ConsoleKey n when (n == ConsoleKey.D4 || n == ConsoleKey.NumPad4):
                    Shop();
                    break;
                case ConsoleKey n when (n == ConsoleKey.D5 || n == ConsoleKey.NumPad5):
                    Quest();
                    break;
                case ConsoleKey n when (n == ConsoleKey.D6 || n == ConsoleKey.NumPad6):
                    SaveGame();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
            }
        }
    }

    static void Status()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("상태 보기");
        Console.ResetColor();
        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

        player.ShowInfo();

        Console.WriteLine("\n0. 나가기\n");

        while(true)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D0:
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
            }
            break;
        }

    }

    static void Inventory()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("인벤토리");
        Console.ResetColor();
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine("[아이템 목록]\n");

        foreach (EquipItem item in player.inventory)
        {
            Console.WriteLine($"- {item.ToString()}");
        }


        Console.WriteLine("\n1. 장착 관리\n0. 나가기\n");

        while (true)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D0:
                    break;
                case ConsoleKey.D1:
                    EqipManage();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
            }
            break;
        }
    }

    static void EqipManage()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");

            int i = 1;
            foreach (Item item in player.inventory)
            {
                Console.WriteLine($"{i++} {item.ToString()}");
            }


            Console.WriteLine("\n0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");

            if(!int.TryParse(Console.ReadLine(), out int num) || num - 1 > player.inventory.Count || num < 0)
            {
                Console.WriteLine("잘못된 입력입니다");     // fix: Console.Clear 후 출력하도록 수정할 것
            }
            else
            {
                if (num == 0) break;
                Item selectedItem = player.inventory[num - 1];

                if (selectedItem is EquipItem equipmentItem)
                {
                    equipmentItem.equip(player);
                }
                else
                {
                    Console.WriteLine("해당 아이템은 장착할 수 없습니다.");
                }
            }
        }
    }

    static void Shop()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("상점");
        Console.ResetColor();
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.gold} G\n");

        Shop shop = new Shop();
        shop.LoadOptions();

        foreach (EquipItem item in shop.items)
        {
            Console.WriteLine($"- {item.ToString()} | {item.price} G");
        }

        Console.WriteLine("\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n");

        while (true)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D0:
                    break;
                case ConsoleKey.D1:
                    BuyItem();
                    break;
                case ConsoleKey.D2:
                    SellItem();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
            }
            break;
        }
    }

    static void BuyItem()
    {
        while(true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G\n");

            shop.LoadOptions();

            int i = 1;
            foreach (EquipItem item in shop.items)
            {
                Console.WriteLine($"{i++} {item.ToString()} | {item.price} G");
            }

            Console.WriteLine("\n0. 나가기\n");
            
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            if (!int.TryParse(Console.ReadLine(), out int num) || num - 1 > shop.items.Count || num < 0)
            {
                Console.WriteLine("잘못된 입력입니다");     // fix: Console.Clear 후 출력하도록 수정할 것
            }
            else
            {
                if (num == 0) break;
                shop.BuyItem(num - 1, player);
            }
        } 
    }

    static void SellItem()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 아이템 판매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G\n");

            int i = 1;
            foreach (EquipItem item in player.inventory)
            {
                Console.WriteLine($"{i++} {item.ToString()} | {(int)(item.price * 0.8)} G");
            }

            Console.WriteLine("\n0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");

            if (!int.TryParse(Console.ReadLine(), out int num) || num - 1 > player.inventory.Count || num < 0)
            {
                Console.WriteLine("잘못된 입력입니다");     // fix: Console.Clear 후 출력하도록 수정할 것
            }
            else
            {
                if (num == 0) break;
                shop.SellItem(num - 1, player);
            }
        }
    }

    static void Quest()
    {
        while (true)
        {
            Console.Clear();
            TextColor("Quest!!\n", ConsoleColor.Yellow);
            
            questboard.LoadOptions();

            int i = 1;
            foreach (Quest quest in questboard.quests)
            {
                Console.WriteLine($"{i++}. {quest.name}");
            }

            Console.WriteLine("\n0. 나가기\n");

            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");

            if (!int.TryParse(Console.ReadLine(), out int num) || num - 1 > questboard.quests.Count || num < 0)
            {
                Console.WriteLine("잘못된 입력입니다");
            }
            else
            {
                if (num == 0) break;
                QuestDetail(num - 1);
            }
        }
    }

    static void QuestDetail(int n)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(questboard.quests[n]);

            if (questboard.quests[n].isActive && questboard.quests[n].isClear)
            {
                Console.WriteLine("1. 보상 받기\n2. 돌아가기\n");
                if (!int.TryParse(Console.ReadLine(), out int num) || num <= 0 || num > 2)
                {
                    Console.WriteLine("잘못된 입력입니다");     // fix: Console.Clear 후 출력하도록 수정할 것
                }
                else
                {
                    if (num == 1)
                    {
                        questboard.ReceiveReward(questboard.quests[n], player);
                        break;
                    }
                    else if (num == 2) break;
                }
            }
            else if (!questboard.quests[n].isActive && !questboard.quests[n].isClear)
            {
                Console.WriteLine("0. 나가기\n1. 수락\n2. 거절\n\n원하시는 행동을 입력해주세요.");
                if (!int.TryParse(Console.ReadLine(), out int num) || num < 0 || num > 2)
                {
                    Console.WriteLine("잘못된 입력입니다");     // fix: Console.Clear 후 출력하도록 수정할 것
                }
                else
                {
                    if (num == 1)
                    {
                        questboard.quests[n].isActive = true;
                        questboard.SaveOptions();
                        break;
                    }
                    else if (num == 2)
                    {
                        questboard.RemoveQuest(questboard.quests[n]);
                        break;
                    }
                    else break;
                }
            }
            else
            {
                Console.WriteLine("퀘스트가 진행중입니다.\n0. 나가기\n");
                if (!int.TryParse(Console.ReadLine(), out int num) || num != 0)
                {
                    Console.WriteLine("잘못된 입력입니다");     // fix: Console.Clear 후 출력하도록 수정할 것
                }
                else break;
            }
        }
    }

    //TextColor("입력할 문구", ConsoleColor.Yellow); 식으로 사용
    public static void TextColor(string text, ConsoleColor clr)
    {
        Console.ForegroundColor = clr;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    static void SaveGame()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };

        string path = Path.Combine(Directory.GetCurrentDirectory(), "player.json");
        string json = JsonConvert.SerializeObject(player, settings);
        File.WriteAllText(path, json);
    }

    static void LoadGame()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };
        string path = Path.Combine(Directory.GetCurrentDirectory(), "player.json");
        if (!File.Exists(path))
        {
            player = CreateCharacter();
            SaveGame();
        }
        string json = File.ReadAllText(path);
        player = JsonConvert.DeserializeObject<Character>(json, settings);
    }
}