using team6_TextGame.Characters;

namespace team6_TextGame.Characters.Monsters
{
    internal class CanonMinion : Monster
    {
        public static int GetMinimumLevel()
        {
            return 3; // 등장할 수 있는 최소 레벨
        }
        /*
        public CanonMinion()
        {
            name = "대포 미니언";   //이름
            level = 5;  //레벨
            atk = 8;    //공격력
            hp = 25;     //체력
            exp = 5;    //경험치 계수
        }
        */
    }
}
