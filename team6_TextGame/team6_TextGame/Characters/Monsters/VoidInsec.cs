﻿using team6_TextGame.Characters;
using team6_TextGame.Items;

namespace team6_TextGame.Characters.Monsters
{
    internal class VoidInsec : Monster
    {
        public static int GetMinimumLevel()
        {
            return 2; // 등장할 수 있는 최소 레벨
        }
        public VoidInsec(string name = "공허충", int level = 3, int atk = 9, int def = 0, int maxHp = 10, int maxMp = 0, int crit = 0, int dodge = 0, int minLv = 2)
            : base(name, level, atk, def, maxHp, maxMp, crit, dodge, minLv)
        {
            dropItems.Add(new EquipItem(2, "무쇠갑옷", 0, 9, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000), 0.1);
        }
    }
}