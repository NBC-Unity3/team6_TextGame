using System.Text;
using team6_TextGame.Characters.Monsters;

namespace team6_TextGame.Characters
{
    internal class Monster : Character
    {
        public int minLv { get; set; }   // 등장할 수 있는 최소 레벨

        private QuestBoard questBoard = new QuestBoard();

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

        public override void Die()
        {
            if (this is Minion)
            {
                Quest thisQuest = questBoard.quests.Find(element => element.name == "마을을 위협하는 미니언 처치");
                if (thisQuest != null && thisQuest.isActive == true) thisQuest.achieve_count++;
            }
        }

    }
}
