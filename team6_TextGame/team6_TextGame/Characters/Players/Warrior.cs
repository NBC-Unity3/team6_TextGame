using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team6_TextGame.Characters.Players
{
    internal class Warrior : Player
    {
        public Warrior()
        {
            atk = 10;
            def = 5;
            hp = 100;
            mp = 50;
            job = "전사";
            f_atk = atk;
            f_def = def;
            f_hp = hp;
            f_mp = mp;
        }

        public override int Skill_1(Monster monster)
        {
            //단일기
            int damage = atk * 2;
            mp -= 10;


            //monster.hp -= damage;
            //Console.WriteLine($"알파 스트라이크!\n{monster.name}에게 {damage}만큼의 대미지를 입혔습니다.");
            return damage;
        }

        public virtual int Skill_2(Monster[] monster)
        {
            //광역기
            int damage = (int)Math.Round(atk * 1.2);
            mp -= 15;

            //string names = "";
            //for(int i = 0; i < monster.Length; i++)
            //{
            //    monster[i].hp -= (int) damage;
            //    names += monster[i].name = " ";
            //}

            //Console.WriteLine($"더블 스트라이크!\n모두에게 {damage}만큼의 대미지를 입혔습니다.");

            return damage;
        }
    }
}
