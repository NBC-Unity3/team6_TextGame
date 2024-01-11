namespace team6_TextGame.Items
{
    internal class HpPotion : ConsumeItem
    {
        public HpPotion()
        {
            id = 1;
            name = "붉은 포션";
            atk = 0;
            def = 0;
            hp = 50;
            info = "체력을 50회복합니다.";
            price = 500;
            count = 3;
        }
    }
}
