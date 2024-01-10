

namespace team6_TextGame.Monsters
{
    internal class Minion : Monster
    {
        public static int GetMinimumLevel()
        {
            return 1; // 등장할 수 있는 최소 레벨
        }
        public Minion()
        {
            this.name = "미니언";
            this.level = 2;
            this.atk = 5;
            this.hp = 15;
            this.exp = 1;
        }
    }
}
