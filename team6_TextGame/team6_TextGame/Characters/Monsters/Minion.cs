using team6_TextGame.Characters;
using team6_TextGame.Items;

namespace team6_TextGame.Characters.Monsters
{
    internal class Minion : Monster
    {
        public static int GetMinimumLevel()
        {
            return 1; // 등장할 수 있는 최소 레벨
        }
        public Minion(string name = "미니언", int level = 2, int atk = 5, int def = 0, int maxHp = 15, int maxMp = 0, int crit = 0, int dodge = 0, int minLv = 1)
            : base (name, level, atk, def, maxHp, maxMp, crit, dodge, minLv)
        {
            dropItems.Add(new EquipItem(1, "수련자 갑옷", 0, 5, 0, "수련에 도움을 주는 갑옷입니다.", 1000), 0.1);
        }
    }
}
