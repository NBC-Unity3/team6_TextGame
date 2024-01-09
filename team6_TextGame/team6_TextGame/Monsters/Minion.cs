using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team6_TextGame.Monsters
{
    internal class Minion : Monster
    {
        public Minion()
        {
            this.name = "미니언";
            this.level = 2;
            this.atk = 5;
            this.hp = 15;
            this.exp = 1;
        }
}
