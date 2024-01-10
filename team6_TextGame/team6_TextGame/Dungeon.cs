using System.Net.Security;
using team6_TextGame.Monsters;

namespace team6_TextGame
{
    internal class Dungeon
    {
        private Character player;
        private int start_player_hp;
        private List<Monster> monsters;
        private Random rand = new Random();
        public int level { get; set; }

        public Dungeon(Character player, int level = 1)
        {
            this.player = player;
            this.level = level;
            monsters = new List<Monster>();

            for (int i = 0; i < rand.Next(level, 4 + level); i++)
            {
                monsters.Add(GenerateRandomMonster());
            }
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
                return minimumLevel <= this.level;
            }).ToList();

            if (availableMonsterGenerators.Count == 0) return null;

            int randomIndex = rand.Next(availableMonsterGenerators.Count);
            return availableMonsterGenerators[randomIndex]();
        }

        public void StartBattle()
        {
            start_player_hp = player.hp; // 던전 들어 올 때의 hp값 저장

            while (player.hp > 0 && monsters.Any(m => m.hp > 0))
            {
                Console.Clear();

                Program.TextColor("Battle!!\n", ConsoleColor.Yellow);
                foreach (var monster in monsters)
                {
                    monster.ShowState();
                }
                Console.WriteLine();

                Program.WriteColoredNumbers($"[내정보]\n" +
                    $"Lv.{player.level} {player.name} ({player.job})\n" +
                    $"HP {player.hp}/100\n\n");

                // Player's turn
                Program.WriteColoredNumbers("1. 공격\n\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey n when (n == ConsoleKey.D1 || n == ConsoleKey.NumPad1):
                        if (Attack())
                            break;
                        else
                            continue;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                }

                // Monsters' turn
                foreach (var monster in monsters)
                {
                    if (player.hp <= 0 || monster.hp <= 0) continue;

                    Console.Clear();

                    Program.TextColor("Battle!!\n", ConsoleColor.Yellow);

                    monster.IsAttack(player);

                    Console.WriteLine();
                    Program.WriteColoredNumbers("0. 다음\n\n");

                    Console.Write(">> ");

                    bool tmp = true;

                    while (tmp)
                    {
                        key = Console.ReadKey(true).Key;
                        switch (key)
                        {
                            case ConsoleKey n when (n == ConsoleKey.D0 || n == ConsoleKey.NumPad0):
                                tmp = false;
                                break;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
                        }
                    }
                }
            }

            // Battle Result
            if (player.hp <= 0)
            {
                DefeatResult();
            }
            else
            {
                VictoryResult();
            }
        }
        private bool Attack()
        {
            Console.Clear();
            Program.TextColor("Battle!!\n", ConsoleColor.Yellow);

            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].hp > 0)
                {
                    //Console.WriteLine($"{i + 1}. {monsters[i].Name}");
                    Program.WriteColoredNumbers($"{i + 1} ");
                    monsters[i].ShowState();
                }
            }
            Console.WriteLine();
            Program.WriteColoredNumbers($"[내정보]\n" +
                $"Lv.{player.level} {player.name} ({player.job})\n" +
                $"HP {player.hp}/100\n\n");

            Program.WriteColoredNumbers("0. 취소\n\n");

            Console.WriteLine("대상을 선택해주세요.");
            Console.Write(">> ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= monsters.Count && monsters[choice - 1].hp > 0)
                {
                    //int player_atk = CalculateAttackDamage(player.f_atk, 0.1f);

                    //player.Attack(monsters[choice - 1]);
                    //PlayerTurnResult(monsters[choice - 1], player_atk);

                    Console.Clear();

                    Program.TextColor("Battle!!\n", ConsoleColor.Yellow);

                    monsters[choice - 1].IsDamaged(player);

                    Console.WriteLine();
                    Program.WriteColoredNumbers("0. 다음\n\n");

                    Console.Write(">> ");

                    while (true)
                    {
                        var key = Console.ReadKey(true).Key;
                        switch (key)
                        {
                            case ConsoleKey n when (n == ConsoleKey.D0 || n == ConsoleKey.NumPad0):
                                return true;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
                        }
                    }
                }
                else
                {
                    if (choice == 0) return false;
                    Console.WriteLine("잘못된 입력입니다");
                }
            }
        }
        /*
         * 해당 부분 Monster Class의 IsDamaged가 같은 역할을 하는 중
        private void PlayerTurnResult(Monster monster, int playerAtk)
        {
            Console.Clear();

            Console.WriteLine("Battle!!\n");

            Console.WriteLine($"{player.name} 의 공격!");
            Console.WriteLine($"Lv.{monster.level} {monster.name} 을(를) 맞췄습니다. [데미지 : {playerAtk}]\n");

            Console.WriteLine($"Lv.{monster.level} {monster.name}");

            if (monster.hp - playerAtk > 0)
                Console.WriteLine($"HP {monster.hp} -> {monster.hp - playerAtk}\n");
            else
                Console.WriteLine($"HP {monster.hp} -> Dead\n");

            Console.WriteLine("0. 다음\n");

            Console.WriteLine(">>");
        }
        */
        /*
         * 해당 부분 Monster Class의 IsAttack이 같은 역할을 하는 중
        private void EnemyTurnResult()
        {

        }
        */
        private void VictoryResult()
        {
            level++;

            Console.Clear();

            Program.TextColor("Battle!! - Result\n", ConsoleColor.Yellow);

            Console.WriteLine("Victory\n");

            Program.WriteColoredNumbers($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.\n\n");

            Program.WriteColoredNumbers($"Lv.{player.level} {player.name}\n");
            Program.WriteColoredNumbers($"HP {start_player_hp} -> {player.hp}\n\n");

            Program.WriteColoredNumbers("0. 다음\n\n");

            Console.Write(">>  ");

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

            Program.TextColor("Battle!! - Result\n", ConsoleColor.Yellow);

            Console.WriteLine("You Lose\n");

            Program.WriteColoredNumbers($"Lv.{player.level} {player.name}\n");
            Program.WriteColoredNumbers($"HP {start_player_hp} -> {player.hp}\n\n");

            Program.WriteColoredNumbers("0. 다음\n\n");

            Console.Write(">>  ");

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
        /*
        private int CalculateAttackDamage(int baseAttack, float errorRatio)
        {
            double error = Math.Ceiling(baseAttack * errorRatio);

            int minAttack = baseAttack - (int)error;
            int maxAttack = baseAttack + (int)error;

            int finalAttack = rand.Next(minAttack, maxAttack + 1);

            return finalAttack;
        }
        */
    }
}