using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team6_TextGame.Characters.Players
{
    internal class Archer : Player
    {
        public Archer(string name, int level = 1, int atk = 12, int def = 3, int hp = 100, int mp = 40, string jop = "궁수", int gold = 1500)
            : base(name, level, atk, def, hp, mp, jop, gold)
        {
        }

        public override int Skill_1(Monster monster)
        {
            //단일기, 명중 부위 따라 데미지가 다른 설정
            Random rand = new Random();

            mp -= 15;

            int part = rand.Next(1, 3);

            int damage = 0;

            switch (part)
            {
                case 1: //급소 관통
                    damage = atk * 3;
                    break;
                case 2: //주요 팔다리 명중
                    damage = (int)Math.Round(atk * 1.2);
                    break;
                case 3: //빗맞음
                    damage = (int)Math.Round(atk * 0.8);
                    break;
            }
            return damage;
        }

        public virtual int Skill_2(Monster[] monster)
        {
            mp -= 5; //자주 사용 가능하게, 하지만 위력 안높음

            //광역기, 불화살로 본대 전체 타격
            int damage = atk;
            return damage;
        }
    }
}
