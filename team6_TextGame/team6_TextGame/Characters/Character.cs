using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace team6_TextGame.Characters
{
    internal class Character
    {
        public string name { get; set; }
        public int level { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int maxHp { get; set; }
        public int maxMp { get; set; }
        public int crit { get; set; }
        public int dodge { get; set; }
        //public int speed { get; set; }


        public int hp { get; set; }     //현재 체력
        public int mp { get; set; }     //현재 마나
        public int f_atk { get; set; }  //현재 공격력
        public int f_def { get; set; }  //현재 방어력


        public bool Attack(Character enemy)
        {
            //UI단
            UI.WriteLine($"{name}의 공격!");
             
            if (dodge > new Random().Next(1, 101))  // 회피율 계산식
            {
                UI.WriteLine($"{enemy.name}이/가 회피했습니다.");
                return false;
            }

            return enemy.Ondamaged(this);
            
            
            //switch (UI.SelectList(new List<string>(new string[] { "- 다음" })))
            //{
            //    case 0:
            //        break;
            //    case -1:
            //        break;
            //}
        }

        public bool Ondamaged(Character enemy, int coefficient = 100)
        {
            int damage;
            int rand = new Random().Next(9, 12); // 안정치 변수

            if (crit > new Random().Next(1, 101)) // 치명타시 방어력 무시 및 20% 추가데미지
            {
                UI.WriteLine("치명적인 공격!");
                damage = (int)(enemy.f_atk * coefficient * rand * 0.0012);
            }
            else    // {ATK*2*(계수/100) - DEF}/2 * 안정치
            {
                damage = (int)((enemy.f_atk * coefficient * 0.02 - f_def) * rand * 0.05);
            }

            hp -= damage;
            UI.WriteLine($"{name}에게 {damage}의 데미지를 가했습니다.");
            if (hp < 0) { hp = 0; }

            return isDead();
        }

        /*
        public void TurnNext()
        {
            Console.WriteLine();
            switch (UI.SelectList(new List<string>(new string[] { "- 다음" })))
            {
                case 0:
                    break;
                case -1:
                    break;
            }
        } */

        public bool isDead()
        {
            if (hp == 0) return true;
            else return false;
        }

        public void Die()
        {
            Console.WriteLine($"{name}가 쓰러졌습니다");
        }

    }
}
