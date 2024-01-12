using Newtonsoft.Json;
using team6_TextGame.Characters;
using team6_TextGame.Characters.Monsters;

namespace team6_TextGame
{
    internal class Dungeon
    {
        private Player player;
        private int start_player_hp;
        private List<Monster> monsters;
        private Random rand = new Random();
        private UI ui = new UI();
        public int floor { get; set; }

        public Dungeon(Player player, int floor = 1)
        {
            this.player = player;
            this.floor = floor;
            LoadDungeon();
            monsters = new List<Monster>();

            InitMonster();
        }

        private Monster GenerateRandomMonster()
        {
            // 현재 던전 레벨에 맞는 몬스터만 필터링
            var availableMonsterGenerators = new List<Func<Monster>>()
            {
                () => new Minion(),
                () => new VoidInsec(),
                () => new CanonMinion()
            }.Where(generator =>
            {
                // 현재 던전 레벨에 적합한지 확인
                var monsterType = generator().GetType();
                int minimumLevel = (int)monsterType.GetMethod("GetMinimumLevel").Invoke(null, null);
                return minimumLevel <= this.floor;
            }).ToList();
            if (availableMonsterGenerators.Count == 0) return null;

            int randomIndex = rand.Next(availableMonsterGenerators.Count);
            return availableMonsterGenerators[randomIndex]();
        }

        public void EnterDungeon()
        {
            //TODO : 5층 단위로 마지막 층수의 던전 불러오기
            // ex) 지난번에 47층에서 포기했다면 45층부터 시작
            //LoadDungeon();
            while (true)
            {
                Console.Clear();

                if (StartBattle())
                {
                    ui.WriteColoredNumbers($"{floor}층을 클리어했습니다");
                    ui.DrawLine();

                    switch (ui.SelectList(new List<string>(new string[] { "- 다음 층으로", "- 돌아간다" })))
                    {
                        case 0:
                            //TODO: 다음 층 불러오기
                            floor++;
                            InitMonster();
                            continue;
                        case 1 or -1:
                            //TODO: 현재 층수 저장
                            if (floor % 5 == 0) SaveDungeon();
                            else LoadDungeon();
                            return;
                    }
                }
                else
                {
                    //TODO: 현재 층수 저장
                    LoadDungeon();
                    return;
                }
            }
        }

        public bool StartBattle()   //승리시 true, 패배시 false 리턴
        {
            while (monsters.Count > 0)
            {
                Console.Clear();
                ui.TextColor("던전 " + floor + "층", ConsoleColor.DarkYellow);
                ui.DrawLine(); Console.WriteLine();

                foreach (var monster in monsters)
                {
                    Console.WriteLine($"   {monster}");
                }

                Console.WriteLine(); ui.DrawLine();
                int menu = Console.CursorTop;

                switch (ui.SelectList(new List<string>(new string[] { "- 공격한다", "- 스킬 사용", "- 아이템 사용", "- 도망가기" }), menu))
                {
                    case 0:
                        Monster target = monsters[ui.SelectList(monsters, 3)];
                        player.Attack(target);
                        if (target.isDead())
                        {
                            target.Die();
                            // TODO: 경험치 획득
                            monsters.Remove(target);    //TODO: 제거 후 리스트 다시 출력할 필요 있음
                        }
                        break;
                    case 1:
                        //TODO: 스킬 구현
                        break;
                    case 2:
                        //TODO: 아이템 사용 구현
                        break;
                    case 3:
                        //TODO: 도망가기 UI
                        return false;
                    case -1:
                        continue;
                }

                if (monsters.Count == 0) break;

                //Enemy turn
                foreach (var monster in monsters)
                {
                    monster.Attack(player);
                    if (player.isDead())
                    {
                        player.Die();
                        return false;
                    }
                }
            }
            return true;
        }

        private void InitMonster()
        {
            int monsterCnt = rand.Next(1 + (int)(floor * 0.2f), 4 + (int)(floor * 0.2f));
            for (int i = 0; i < monsterCnt; i++)
            {
                monsters.Add(GenerateRandomMonster());
            }
        }

        private void VictoryResult()
        {
            floor++;
            SaveDungeon();
            Console.Clear();

            Console.WriteLine("Battle!! - Result\n");

            Console.WriteLine("Victory\n");

            Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.\n");

            Console.WriteLine($"Lv.{player.level} {player.name}");
            Console.WriteLine($"HP {start_player_hp} -> {player.hp}\n");

            Console.WriteLine("0. 다음\n");

            Console.WriteLine(">>");

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey n when (n == ConsoleKey.D0 || n == ConsoleKey.NumPad0):
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }
        private void DefeatResult()
        {
            Console.Clear();

            Console.WriteLine("Battle!! - Result\n");

            Console.WriteLine("You Lose\n");

            Console.WriteLine($"Lv.{player.level} {player.name}");
            Console.WriteLine($"HP {start_player_hp} -> {player.hp}\n");

            Console.WriteLine("0. 다음\n");

            Console.WriteLine(">>");

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey n when (n == ConsoleKey.D0 || n == ConsoleKey.NumPad0):
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }


        private void SaveDungeon()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            string path = Path.Combine(Directory.GetCurrentDirectory(), "dungeon.json");
            string json = JsonConvert.SerializeObject(floor, settings);
            File.WriteAllText(path, json);

            /*
            string path = Path.Combine(Directory.GetCurrentDirectory(), "dungeon.csv");
            File.WriteAllText(path, floor.ToString());
            */
        }
        private void LoadDungeon()
        {

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            string path = Path.Combine(Directory.GetCurrentDirectory(), "dungeon.json");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                int loadlevel = JsonConvert.DeserializeObject<int>(json, settings);
                floor = loadlevel;
            }

            /*
            string path = Path.Combine(Directory.GetCurrentDirectory(), "dungeon.csv");
            if (File.Exists(path))
            {
                string csvContent = File.ReadAllText(path);
                if (int.TryParse(csvContent, out int loadLevel))
                {
                    floor = loadLevel;
                }
            }
            */
        }
    }
}