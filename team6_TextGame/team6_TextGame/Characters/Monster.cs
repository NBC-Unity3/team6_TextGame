namespace team6_TextGame.Characters
{
    internal class Monster : Character
    {
        public string name { get; set; }   //이름
        public int level { get; set; }  //레벨
        public int atk { get; set; }    //공격력
        public int hp { get; set; }     //체력
        public int exp { get; set; }    //경험치 계수


        public void ShowState() // 현재 상태를 출력
        {
            if (hp > 0)
            {
                Console.WriteLine("Lv. " + level + " " + name + "  HP " + hp);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Lv. " + level + " " + name + " Dead ");
                Console.ResetColor();
            }
        }

        public void IsDamaged(Player character)    // 몬스터가 공격받는 함수
        {
            Random rand = new Random(); // 공격력의 10% 오차를 내기위한 랜덤 변수
            int damage = rand.Next(9, 12);  // 9~11의 랜덤한 값에 0.1을 곱하는 방식으로 구함

            Console.WriteLine(character.name + "의 공격!");
            Console.WriteLine("Lv. " + level + " " + name + "을(를) 맞췄습니다. [데미지 : " + (int)(character.atk * (0.1 * damage)) + "]");
            Console.WriteLine("");
            Console.WriteLine("Lv. " + level + " " + name);
            Console.Write("HP " + hp + " -> ");
            hp -= (int)(character.atk * (0.1 * damage));
            if (hp > 0)
                Console.WriteLine("HP " + hp);  // 체력이 남아있으면 남은 체력을 출력
            else
                Console.WriteLine("Dead");    // 체력이 다 떨어지면 죽는 Dead 텍스트를 출력
        }

        public void IsAttack(Player character)   //몬스터가 공격하는 함수
        {
            if (hp > 0)
            {
                Console.WriteLine("Lv. " + level + " " + name + "의 공격!");
                Console.WriteLine(character.name + "을(를) 맞췄습니다. [데미지 : " + atk + "]");
                Console.WriteLine("");
                Console.WriteLine("Lv. " + character.level + " " + character.name);
                Console.Write("HP " + character.hp + " -> ");
                character.hp -= atk;
                if (character.hp > 0)
                    Console.WriteLine("HP " + character.hp);  // 체력이 남아있으면 남은 체력을 출력
                else
                {
                    character.hp = 0;
                    Console.WriteLine("Dead");  // 체력이 다 떨어지면 죽는 Dead 텍스트를 출력
                }
            }
        }
    }
}
