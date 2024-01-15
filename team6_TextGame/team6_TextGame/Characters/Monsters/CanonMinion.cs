using System.Diagnostics;
using team6_TextGame.Characters;
using team6_TextGame.Items;

namespace team6_TextGame.Characters.Monsters
{
    internal class CanonMinion : Monster
    {
        public static int GetMinimumLevel()
        {
            return 3; // 등장할 수 있는 최소 레벨
        }
        public CanonMinion(string name = "대포 미니언", int level = 5, int atk = 8, int def = 0, int maxHp = 25, int maxMp = 0, int crit = 0, int dodge = 0, int minLv = 3)
            : base (name, level, atk, def, maxHp, maxMp, crit, dodge, minLv)
        {
            dropItems.Add(new EquipItem(3, "스파르타의 갑옷", 0, 15, 0, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500), 0.1);
        }
    }
}