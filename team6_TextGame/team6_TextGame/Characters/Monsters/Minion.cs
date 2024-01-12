using team6_TextGame.Characters;

namespace team6_TextGame.Characters.Monsters
{
    internal class Minion : Monster
    {
        public static int GetMinimumLevel()
        {
            return 1; // 등장할 수 있는 최소 레벨
        }
        public Minion(string name = "미니언", int level = 2, int atk = 5, int def = 0, int maxHp = 15, int maxMp = 0, int crit = 0, int dodge = 0, int exp = 1, int minLv = 1)
            : base (name, level, atk, def, maxHp, maxMp, crit, dodge, exp, minLv)
        {
        }
    }
}
