﻿

namespace team6_TextGame.ConsumableItem
{
    internal class AtkPotion : ConsumableItem
    {
        public AtkPotion()
        {
            id = 2;
            name = "전사의 영약";
            atk = 3;
            def = 0;
            hp = 0;
            info = "영구히 공격력이 3 상승합니다.";
            price = 2500;
            count = 0;
        }
    }
}