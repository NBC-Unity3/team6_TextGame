using System.Text;

namespace team6_TextGame.Characters
{
    internal class Monster : Character
    {
        public int minLv { get; set; }   // 등장할 수 있는 최소 레벨

        public Dictionary<Item, double> dropItems = new Dictionary<Item, double>();

        public Monster(string name, int level, int atk, int def, int maxHp, int maxMp, int crit, int dodge, int minLv)
        {
            this.name = name;
            this.level = level;
            this.atk = atk;
            this.def = def;
            this.maxHp = maxHp;
            this.maxMp = maxMp;
            this.crit = crit;
            this.dodge = dodge;
            this.minLv = minLv;

            hp = maxHp;
            mp = maxMp;
            f_atk = atk;
            f_def = def;
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Lv.");
            sb.Append(level);
            sb.Append(" ");
            sb.Append(name);
            sb.Append(" HP ");
            sb.Append(hp);
            sb.Append("/");
            sb.Append(maxHp);

            return sb.ToString();
        }

    }
}
