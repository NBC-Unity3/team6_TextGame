using team6_TextGame.Characters;

namespace team6_TextGame.Characters.Monsters
{
    internal class VoidInsec : Monster
    {
        public static int GetMinimumLevel()
        {
            return 2; // 등장할 수 있는 최소 레벨
        }
        public VoidInsec()
        {
            name = "공허충";   //이름
            level = 3;  //레벨
            atk = 9;    //공격력
            hp = 10;     //체력
            exp = 3;    //경험치 계수
        }
    }
}
