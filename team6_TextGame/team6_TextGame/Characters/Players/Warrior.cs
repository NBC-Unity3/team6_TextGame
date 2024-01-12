using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team6_TextGame.Characters.Players
{
    internal class Warrior : Player
    {
        public Warrior(string name, int atk = 10, int def = 5, int hp = 100, int mp = 50, string jop = "전사")
            : base(name, atk, def, hp, mp, jop)
        {
        }

        public override void Skill_1(Monster monster)
        {
            Console.Clear();
            //단일기
            Console.WriteLine($"전사의 검술! {name}이 단일 공격을 시전했습니다.\n");
            ChangeMP(-10);
            monster.Ondamaged(this, 200);
            TurnNext();
            //Console.WriteLine($"알파 스트라이크!\n{monster.name}에게 {damage}만큼의 대미지를 입혔습니다.");
        }

        public override void Skill_2(List<Monster> monsters)
        {
            Console.Clear();
            Console.WriteLine($"전사의 포효! {name}이 광역 공격을 시전했습니다.\n");
            //광역기
            ChangeMP(-15);

            //두마리만 데미지 주기
            int cnt = 0;

            while(cnt <= 2)
            {
                Random rand = new Random();
                int i = rand.Next(0, monsters.Count - 1);

                monsters[i].Ondamaged(this, 120);
                monsters.RemoveAt(i);
                cnt++;
            }

            TurnNext();
        }
    }
}
