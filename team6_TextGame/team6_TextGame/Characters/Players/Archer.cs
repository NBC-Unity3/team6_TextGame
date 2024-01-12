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
            mp -= 15;

            Random rand = new Random();
            int part = rand.Next(1, 3);

            switch (part)
            {
                case 1: //급소 관통
                    monster.Ondamaged(this, 300);
                    break;
                case 2: //주요 팔다리 명중
                    monster.Ondamaged(this, 120);
                    break;
                case 3: //빗맞음
                    monster.Ondamaged(this, 80);
                    break;
            }
            //Console.WriteLine($"{monster.name}에게 {damage}의 데미지를 입혔습니다.");
        }

        public virtual void Skill_2(List<Monster> monsters)
        {
            mp -= 5; //자주 사용 가능하게, 하지만 위력 안높음

            //광역기, 불화살로 본대 전체 타격
            foreach(Monster m in monsters)
            {
                m.Ondamaged(this, 80);
            }
        }
    }
}
