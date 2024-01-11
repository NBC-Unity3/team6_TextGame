using team6_TextGame.Characters;

namespace team6_TextGame.Characters.Monsters
{
    internal class Minion : Monster
    {
        public static int GetMinimumLevel()
        {
            return 1; // 등장할 수 있는 최소 레벨
        }
        public Minion()
        {
            name = "미니언";
            level = 2;
            atk = 5;
            hp = 15;
            exp = 1;
        }
    }
}
