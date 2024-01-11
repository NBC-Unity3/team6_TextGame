using System;
using System.Collections.Generic;
using System.Linq;
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
        public int maxhp { get; set; }
        public int maxmp { get; set; }
        public int crit { get; set; }
        public int dodge { get; set; }

        //public int speed { get; set; }


        public int hp { get; set; }
        public int mp { get; set; }
        public int f_atk { get; set; }
        public int f_def { get; set; }


        public void Attack(Character enemy)
        {
            //UI단
            Console.WriteLine(name + "의 공격!");

        }

        public void Ondamaged(Character enemy, int coefficient = 100)
        {
            int damage;
            int rand = new Random().Next(9, 12); // 안정치 변수

            if (dodge > new Random().Next(1, 101))  // 회피율 계산식
            {
                Console.WriteLine("회피");
                return;
            }

            if (crit > new Random().Next(1, 101)) // 치명타시 방어력 무시 및 20% 추가데미지
            {
                Console.WriteLine("치명적인 공격!");
                damage = (int)(enemy.f_atk * coefficient * rand * 0.0012);
            }
            else    // {ATK*2*(계수/100) - DEF}/2 * 안정치
            {
                damage = (int)((enemy.f_atk * coefficient * 0.02 - f_def) * rand * 0.05);
            }

            hp -= damage;
            if (hp < 0) Die();

        }

        public virtual void Die()
        {
            // 자식 클래스에서 구현
        }

    }
}
