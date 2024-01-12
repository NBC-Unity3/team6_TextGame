using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team6_TextGame.Characters.Players
{
    internal class Archer : Player
    {
        public Archer(string name, int atk = 12, int def = 3, int hp = 100, int mp = 40, string jop = "궁수")
            : base(name, atk, def, hp, mp, jop)
        {
        }

        public override void Skill_1(Monster monster)
        {
            //단일기, 명중 부위 따라 데미지가 다른 설정
            ChangeMP(-15);

            Random rand = new Random();
            int part = rand.Next(1, 100);

            //확률 수정
            if(part <= 5) //급소 관통
            {
                monster.Ondamaged(this, 300);
            } else if(part > 5 && part <= 80) //주요 팔다리 명중
            {
                monster.Ondamaged(this, 120);
            }
            else //빗맞음
            {
                monster.Ondamaged(this, 80);
            }
            //Console.WriteLine($"{monster.name}에게 {damage}의 데미지를 입혔습니다.");
        }

        public override void Skill_2(List<Monster> monsters)
        {
            //광역기, 한발마다 mp 닳게 몬스터 숫자 비례 mp 사용
            foreach(Monster m in monsters)
            {
                ChangeMP(-5);
                m.Ondamaged(this, 80);
            }
        }
    }
}
