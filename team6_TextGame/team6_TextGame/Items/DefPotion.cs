﻿namespace team6_TextGame.Items
{
    internal class DefPotion : ConsumeItem
    {
        public DefPotion()
        {
            id = 13;
            name = "수호자의 영약";
            atk = 0;
            def = 3;
            hp = 0;
            info = "방어력이 영구히 3 상승합니다.";
            price = 2500;
            count = 0;
            type = 3;
        }
    }
}
