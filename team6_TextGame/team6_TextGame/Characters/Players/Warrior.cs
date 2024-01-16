using System;
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

        public override void Skill_1(List<Monster> monsters, int menu)
        {
            if(EnoughMp(10))
            {
                Character monster = monsters[UI.SelectList(monsters, false, 3)];
                UI.Clear(menu);
                UI.WriteLine($"전사의 방패강타!");
                if (isDodged(monster)) return;

                if (monster.Ondamaged(this, 120))
                {
                    monster.Die();
                    monsters.Remove((Monster)monster);
                }
            }
            UI.Wait();
        }

        public override void Skill_2(List<Monster> monsters, int menu)
        {
            if (EnoughMp(15))
            {
                UI.Clear(menu);
                UI.WriteLine($"전사의 워크라이!");

                foreach (Monster monster in monsters)
                {
                    if (isDodged(monster)) return;

                    if (monster.Ondamaged(this, 90))
                    {
                        monster.Die();
                        monsters.Remove((Monster)monster);
                    }
                }

                /*
                int cnt = 0;
                while (cnt < 2 && monsters.Count > 0)
                {
                    Random rand = new Random();
                    int i = rand.Next(0, monsters.Count - 1);

                    monsters[i].Ondamaged(this, 90);
                    monsters.RemoveAt(i);
                    cnt++;
                }
                */
            }
            UI.Wait();
        }
    }
}
