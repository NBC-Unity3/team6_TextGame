using team6_TextGame;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Numerics;
using team6_TextGame.Items;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using team6_TextGame.Characters;
using team6_TextGame.Characters.Players;


class Program
{
    static QuestBoard questboard = new QuestBoard();
    static Shop shop = new Shop();
    static Player player;
    static Dungeon dungeon;

    static void Main(String[] args)
    {
        LoadGame();
        dungeon = new Dungeon(player);
        StartGame();
    }

    static Player CreateCharacter()
    {
        string name;
        //이름 입력
        while(true)
        {
            Console.Clear();
            Console.WriteLine("당신의 이름은 무엇입니까?");
            name = Console.ReadLine();
            Console.WriteLine("\n'{0}' 이 당신의 이름이 맞습니까?\n", name);

            int cmd = UI.SelectList(new List<string>(new string[] { "1. 맞습니다.", "2. 아닙니다." }));
            if (cmd == 0)
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

        while (true)
        {
            int cmd = UI.SelectList(new List<string>(new string[] { "1. 전사", "2. 궁수", "3. 마법사" }));
            if (cmd == 0)
            {
                player = new Warrior(name);
            }
            else if (cmd == 1)
            {
                player = new Archer(name);
            }
            else if (cmd == 2)
            {
                player = new Mage(name);
            }
            else
            {
                continue;
            }

            break;
        }

        return player;
    }

    /*
    static void StartGame()
    {
        while (true) {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine($"1. 상태 보기\n2. 전투 시작 (현재 진행 : {dungeon.floor}층)\n3. 인벤토리\n4. 상점\n5. 퀘스트\n6. 저장하기\n\n");

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
    */

    static void StartGame()
    {
        while (true)
        {
            Console.Clear();
            UI.TextColor("마을", ConsoleColor.Yellow);
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            UI.DrawLine();

            switch (UI.SelectList(new List<string>(new string[] { "1.상태보기", "2.전투 시작(현재 진행 : "+ dungeon.floor + "층)", "3.인벤토리", "4.상점", "5.퀘스트", "6.저장" })))
            {
                case 0:
                    Status();
                    break;
                case 1:
                    dungeon.InitMonster();
                    dungeon.EnterDungeon();
                    break;
                case 2:
                    Inventory();
                    break;
                case 3:
                    Shop();
                    break;
                case 4:
                    Quest();
                    break;
                case 5: 
                    SaveGame();
                    break;
                case -1:
                    return;
            }
        }
    }



    static void Status()
    {
        /*
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
        */

        Console.Clear();
        UI.TextColor("상태 보기", ConsoleColor.Yellow);
        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
        UI.DrawLine();

        player.ShowInfo();

        while (true)
        {
            Console.WriteLine();
            switch (UI.SelectList(new List<string>(new string[] { "0. 나가기" })))
            {
                case 0:
                    return;
                case -1:
                    return;
            }
        }
    }

    static void Inventory()
    {
        while (true)
        {
            Console.Clear();
            UI.TextColor("인벤토리", ConsoleColor.Yellow);
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            UI.DrawLine();

            switch (UI.SelectList(new List<string>(new string[] { "- 장비 아이템 관리", "- 소비 아이템 관리" })))
            {
                case 0:
                    EquipManage();
                    break;
                case 1:
                    ConsumeManage();
                    break;
                case -1:
                    return;
            }
        }
    }

    static void EquipManage()
    {
        while (true)
        {
            Console.Clear();
            UI.TextColor("인벤토리 - 장비 아이템", ConsoleColor.Yellow);
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            UI.DrawLine();

            int index = UI.SelectList(player.equips);
            if (index == -1) return;

            Console.Clear();
            UI.TextColor("선택한 아이템:", ConsoleColor.Yellow);
            Console.WriteLine($"{player.equips[index].ToString()}\n");
            UI.DrawLine();

            switch (UI.SelectList(new List<string>(new string[] { "- 장착/장착해제" })))
            {
                case 0:
                    player.equips[index].equip(player);
                    foreach (Quest q in questboard.quests)
                    {
                        if (player.equips[index].isEquipped == true)
                        {
                            if (q.name == "장비를 장착해보자" && q.isActive == true)
                            {
                                q.achieve_count = 1;
                                questboard.SaveOptions();
                            }
                        }
                    }
                    break;
                case -1:
                    break;
            }
        }
    }

    static void ConsumeManage()
    {
        while (true)
        {
            Console.Clear();
            UI.TextColor("인벤토리 - 소비 아이템", ConsoleColor.Yellow);
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            UI.DrawLine();

            int index = UI.SelectList(player.consumes);
            if (index == -1) return;
            UI.DrawLine();

            switch (UI.SelectList(new List<string>(new string[] { "- 사용" })))
            {
                case 0:
                    // write code
                    break;
                case -1:
                    break;
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

        foreach (Item item in shop.items)
        {
            Console.WriteLine($"- {item.ToString()} | {item.price} G");
        }
        Console.WriteLine("");


        while (true)
        {
            switch (UI.SelectList(new List<string>(new string[] { "1. 아이템 구매", "2. 아이템 판매", "0.나가기" })))
            {
                case 0:
                    BuyItem();
                    break;
                case 1:
                    SellItem();
                    break;
                case 2:
                    break;
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

            int index = UI.SelectList(shop.items);

            switch (UI.SelectList(new List<string>(new string[] { "- 아이템 구매" })))
            {
                case 0:
                    shop.BuyItem(index, player);
                    SaveGame();
                    break;
                case -1:
                    return;
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

            int index = UI.SelectList(player.equips);

            switch (UI.SelectList(new List<string>(new string[] { "- 아이템 판매" })))
            {
                case 0:
                    shop.SellItem(index, player);
                    break;
                case -1:
                    return;
            }

            //int i = 1;
            //foreach (EquipItem item in player.equips)
            //{
            //    Console.WriteLine($"{i++} {item.ToString()} | {(int)(item.price * 0.8)} G");
            //}

            //Console.WriteLine("\n0. 나가기\n");

            //Console.WriteLine("원하시는 행동을 입력해주세요.");

            //if (!int.TryParse(Console.ReadLine(), out int num) || num - 1 > player.equips.Count || num < 0)
            //{
            //    Console.WriteLine("잘못된 입력입니다");     // fix: Console.Clear 후 출력하도록 수정할 것
            //}
            //else
            //{
            //    if (num == 0) break;
            //    shop.SellItem(num - 1, player);
            //}
        }
    }

    static void Quest()
    {
        while (true)
        {
            Console.Clear();
            UI.TextColor("Quest!!\n", ConsoleColor.Yellow);
            Console.WriteLine("퀘스트를 확인할 수 있습니다.(나가기: esc)\n");
            UI.DrawLine();
            questboard.LoadOptions();
            questboard.ClearCheck(player);

            int index = UI.SelectList(questboard.quests);

            if (index >= 0)
            {
                QuestDetail(index);
            }
            else break;
        }
    }

    static void QuestDetail(int n)
    {
        while (true)
        {
            Console.Clear();
            UI.TextColor("Quest!!\n", ConsoleColor.Yellow);
            Console.WriteLine(questboard.quests[n]);
            UI.DrawLine();

            if (questboard.quests[n].isActive && questboard.quests[n].isClear)
            {
                switch (UI.SelectList(new List<string>(new string[] { "보상 받기"})))
                {
                    case 0:
                        questboard.ReceiveReward(questboard.quests[n], player);
                        return;
                    case -1:
                        return;
                }
            }
            else if (!questboard.quests[n].isActive && !questboard.quests[n].isClear)
            {
                switch (UI.SelectList(new List<string>(new string[] { "수락", "거절" })))
                {
                    case 0:
                        questboard.quests[n].isActive = true;
                        questboard.SaveOptions();
                        break;
                    case 1:
                        questboard.RemoveQuest(questboard.quests[n]);
                        return;
                    case -1:
                        return;
                }
            }
            else
            {
                switch (UI.SelectList(new List<string>(new string[] { "퀘스트가 진행중입니다." })))
                {
                    case -1:
                        return;
                }
            }
        }
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
        else
        {
            string json = File.ReadAllText(path);
            Player save = JsonConvert.DeserializeObject<Player>(json, settings);

            if (save.job == "전사")
            {
                player = JsonConvert.DeserializeObject<Warrior>(json, settings);
            } else if(save.job == "궁수")
            {
                player = JsonConvert.DeserializeObject<Archer>(json, settings);
            }
            else
            {
                player = JsonConvert.DeserializeObject<Mage>(json, settings);
            }
        }
    }
}