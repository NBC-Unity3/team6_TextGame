using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using team6_TextGame.Characters;
using team6_TextGame.Characters.Monsters;
using team6_TextGame.Items;

namespace team6_TextGame
{
    internal class Dungeon
    {
        private Player player;
        private QuestBoard questBoard;
        private int start_player_hp;
        private List<Monster> monsters;
        private Random rand = new Random();
        public int floor { get; set; }

        public Dungeon(Player player, QuestBoard questBoard, int floor = 1)
        {
            this.player = player;
            this.questBoard = questBoard;
            this.floor = floor;
            LoadDungeon();
            monsters = new List<Monster>();
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
            while (true)
            {
                Console.Clear();

                // 보상 미리 구해두기, 배틀에서 죽은 몬스터는 배열에서 빠지기 때문
                int goldReward = CalculateGoldReward(monsters);
                int expReward = CalculateExpReward(monsters);
                var itemReward = CalculateItemReward(monsters);

                if (StartBattle())
                {
                    Console.Clear();

                    UI.WriteColoredNumbers($"{floor}층을 클리어했습니다\n");
                    UI.DrawLine();

                    // 보상 획득
                    player.ReceiveGold(goldReward);
                    player.ReceiveExp(expReward);
                    player.ReceiveItem(itemReward);

                    switch (UI.SelectList(new List<string>(new string[] { "- 다음 층으로", "- 돌아간다" })))
                    {
                        case 0:
                            //TODO: 다음 층 불러오기
                            floor++;
                            // 5층 마다 던전 층 저장
                            if (floor % 5 == 0) SaveDungeon();
                            InitMonster();
                            continue;
                        case 1 or -1:
                            //TODO: 현재 층수 저장
                            // 5층 마다 던전 층 저장
                            if ((floor + 1) % 5 == 0) SaveDungeon();
                            LoadDungeon();
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
                int c = 0;
                Console.Clear();
                UI.TextColor("던전 " + floor + "층", ConsoleColor.DarkYellow);
                UI.DrawLine(); Console.WriteLine();

                foreach (var monster in monsters)
                {
                    UI.WriteColoredNumbers($"   {monster}\n");
                }

                Console.WriteLine(); UI.DrawLine();
                player.ShowStatus(); Console.WriteLine(); UI.DrawLine();

                int menu = Console.CursorTop;

                while(true)
                {
                    switch (UI.SelectList(new List<string>(new string[] { "- 공격한다", "- 스킬 사용", "- 아이템 사용", "- 도망가기" }), menu))
                    {
                        case 0:
                            int index = UI.SelectList(monsters, 3);
                            if (index == -1) continue;
                            Monster target = monsters[index];
                            UI.Clear(menu, 8);
                            if (player.Attack(target))
                            {
                                if (target is Minion)
                                {
                                    Quest thisQuest = questBoard.quests.Find(element => element.name == "마을을 위협하는 미니언 처치");
                                    if (thisQuest != null && thisQuest.isActive == true) thisQuest.achieve_count++;
                                }
                                target.Die();
                                monsters.Remove(target);    //TODO: 제거 후 리스트 다시 출력할 필요 있음
                            }
                            break;
                        case 1:
                            //TODO: 스킬 1,2 출력
                            if (player.mp > 0)
                            {
                                switch (UI.SelectList(new List<string>(new string[] { "- 단일 공격", "- 광역 공격" })))
                                {
                                    case 0:
                                        target = monsters[UI.SelectList(monsters, 3)];
                                        UI.Clear(menu, 8);
                                        player.Skill_1(target);
                                        if (target.isDead())
                                        {
                                            if (target is Minion)
                                            {
                                                Quest thisQuest = questBoard.quests.Find(element => element.name == "마을을 위협하는 미니언 처치");
                                                if (thisQuest != null && thisQuest.isActive == true) thisQuest.achieve_count++;
                                            }
                                            target.Die();
                                            // TODO: 경험치 획득
                                            monsters.Remove(target);    //TODO: 제거 후 리스트 다시 출력할 필요 있음
                                        }
                                        break;
                                    case 1:
                                        UI.Clear(menu, 8);
                                        player.Skill_2(monsters);
                                        for (int i = monsters.Count - 1; i >= 0; i--)
                                        {
                                            if (monsters[i].isDead())
                                            {
                                                if (monsters[i] is Minion)
                                                {
                                                    Quest thisQuest = questBoard.quests.Find(element => element.name == "마을을 위협하는 미니언 처치");
                                                    if (thisQuest != null && thisQuest.isActive == true) thisQuest.achieve_count++;
                                                }
                                                monsters[i].Die();
                                                monsters.RemoveAt(i);
                                            }
                                        }
                                        break;
                                }
                            }
                            else // TODO: 안내 출력 안됨
                            {
                                Console.WriteLine("MP가 부족해 스킬을 사용할 수 없습니다.");
                                continue;
                            }
                            break;
                        case 2:
                            //TODO: 아이템 사용 구현
                            c = UI.SelectList(player.consumes);
                            if (c == -1) break;
                            UI.Clear(menu, 12);
                            player.UseItem(player.consumes[c]);
                            break;
                        case 3:
                            //TODO: 도망가기 UI
                            return false;
                        case -1:
                            continue;
                    }
                    if (c != -1)
                    {
                        UI.Clear(menu, 8);
                        break;
                    }
                    else
                    {
                        c = 0;
                        UI.Clear(menu, 8);
                    }
                }
                
                UI.Wait();
                if (monsters.Count == 0) break;


                //Enemy turn
                UI.Clear(menu, 8);
                foreach (var monster in monsters)
                {
                    if (monster.Attack(player))
                    {
                        player.Die();
                        return false;
                    }
                    Console.WriteLine();
                    UI.Wait();
                }
            }
            return true;
        }

        public void InitMonster()
        {
            monsters.Clear();
            int monsterCnt = 0;
            if ((int)(floor * 0.2f) < 5)
                monsterCnt = rand.Next(1 + (int)(floor * 0.2f), 4 + (int)(floor * 0.2f));
            else
                monsterCnt = rand.Next(6, 9);
            for (int i = 0; i < monsterCnt; i++)
            {
                monsters.Add(GenerateRandomMonster());
            }
        }
        
        private int CalculateGoldReward(List<Monster> monsters)
        {
            int reward = 0;

            foreach (var monster in monsters)
            {
                reward += rand.Next(monster.level * 30, monster.level * 60);
            }

            return reward;
        }
        
        private int CalculateExpReward(List<Monster> monsters)
        {
            int reward = 0;

            foreach (var monster in monsters)
            {
                reward += monster.level;
            }

            return reward;
        }

        private List<Item> CalculateItemReward(List<Monster> monsters)
        {
            List<Item> reward = new List<Item>();
            foreach (Monster monster in monsters)
            {
                foreach (var dropItem in monster.dropItems)
                {
                    if (rand.NextDouble() < dropItem.Value)
                    {
                        reward.Add(dropItem.Key);
                    }
                }
            }
            return reward;
        }

        private void VictoryResult()
        {
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