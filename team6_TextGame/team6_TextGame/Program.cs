using NBC_TextGame;
using Newtonsoft.Json;


class Program
{
    static Shop shop = new Shop();
    static Character player = new Character();

    static void Main(String[] args)
    {
        LoadGame();

        StartGame();
    }


    static void StartGame()
    {
        while (true) {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 저장하기\n\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                    Status();
                    break;
                case ConsoleKey.D2:
                    Inventory();
                    break;
                case ConsoleKey.D3:
                    Shop();
                    break;
                case ConsoleKey.D4:
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

        foreach (Item item in player.inven)
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
            foreach (Item item in player.inven)
            {
                Console.WriteLine($"{i++} {item.ToString()}");
            }


            Console.WriteLine("\n0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");

            if(!int.TryParse(Console.ReadLine(), out int num) || num - 1 > player.inven.Count || num < 0)
            {
                Console.WriteLine("잘못된 입력입니다");     // fix: Console.Clear 후 출력하도록 수정할 것
            }
            else
            {
                if (num == 0) break;
                player.inven[num - 1].eqip(player);
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
            foreach (Item item in shop.items)
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
            foreach (Item item in player.inven)
            {
                Console.WriteLine($"{i++} {item.ToString()} | {(int)(item.price * 0.8)} G");
            }

            Console.WriteLine("\n0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");

            if (!int.TryParse(Console.ReadLine(), out int num) || num - 1 > player.inven.Count || num < 0)
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


    static void SaveGame()
    {
        string path = System.IO.Directory.GetCurrentDirectory() + "/player.json";
        string json = JsonConvert.SerializeObject(player, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    static void LoadGame()
    {
        string path = System.IO.Directory.GetCurrentDirectory() + "/player.json";
        if(!File.Exists(path)) SaveGame();

        string json = File.ReadAllText(path);

        Character data = JsonConvert.DeserializeObject<Character>(json);

        player = data;
    }


}