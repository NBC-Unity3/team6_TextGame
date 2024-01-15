﻿using System;
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
            //단일기
            if(ChangeMP(-10))
            {
                UI.WriteLine($"전사의 검술! {name}이 단일 공격을 시전했습니다.\n");
                monster.Ondamaged(this, 200);
            }
        }

        public override void Skill_2(List<Monster> monsters)
        {
            if(ChangeMP(-15))
            {
                UI.WriteLine($"전사의 포효! {name}이 광역 공격을 시전했습니다.\n");
                //광역기
                //두마리만 데미지 주기
                int cnt = 0;

                while (cnt < 2 && monsters.Count > 0)
                {
                    Random rand = new Random();
                    int i = rand.Next(0, monsters.Count - 1);

                    monsters[i].Ondamaged(this, 120);
                    monsters.RemoveAt(i);
                    cnt++;
                }
            }
        }
    }
}
