using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team6_TextGame.Characters.Players
{
    internal class Mage : Player
    {
        public Mage(string name, int atk = 8, int def = 4, int hp = 100, int mp = 70, string jop = "마법사")
            : base(name, atk, def, hp, mp, jop)
        {
        }

        public override int Skill_1(Monster monster)
        {
            //속성 단일 공격 -> 몬스터마다 다르게 설정하고 싶은데 몬스터 받아오는 형태로 여기서 공격할지
            //                  아니면 그냥 진짜 데미지값만 리턴할지 모르겠음
            int damage = atk * 2;
            mp -= 10;

            return damage;
        }

        public virtual int Skill_2(Monster[] monster)
        {
            //광역기
            int damage = (int)Math.Round(atk * 1.5);
            return damage;
        }
    }
}
