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

        public override void Skill_1(Monster monster)
        {
            ChangeMP(-15);
            //상대 공격력 비례 공격 5 8 9
            int coef = monster.atk * 8 + 100; // 140 164 172
            monster.Ondamaged(this, coef);
        }

        public override void Skill_2(List<Monster> monsters)
        {
            //단순 광역기
            ChangeMP(-20);
            foreach(Monster m in monsters)
            {
                m.Ondamaged(this, 120);
            }
        }
    }
}
