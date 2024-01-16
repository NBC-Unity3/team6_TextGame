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

        public override void Skill_1(List<Monster> monsters, int menu)
        {
            /*
            if(ChangeMP(-15))
            {
                UI.WriteLine($"마법사의 냉기! {name}이 단일 공격을 시전했습니다.\n");
                //상대 공격력 비례 공격 5 8 9
                int coef = monster.atk * 8 + 100; // 140 164 172
                monster.Ondamaged(this, coef);
            }
            */
        }

        public override void Skill_2(List<Monster> monsters, int menu)
        {
            /*
            if(ChangeMP(-20))
            {
                UI.WriteLine($"마법사의 불구덩이! {name}이 광역 공격을 시전했습니다.\n");
                //단순 광역기
                foreach (Monster m in monsters)
                {
                    if (mp == 0) break;
                    m.Ondamaged(this, 120);
                }
            }
            */
        }
    }
}
