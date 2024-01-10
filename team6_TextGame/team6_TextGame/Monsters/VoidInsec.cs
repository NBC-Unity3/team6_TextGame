

namespace team6_TextGame.Monsters
{
    internal class VoidInsec : Monster
    {
        public static int GetMinimumLevel()
        {
            return 2; // 등장할 수 있는 최소 레벨
        }
        public VoidInsec()
        {
            this.name = "공허충";   //이름
            this.level = 3;  //레벨
            this.atk = 9;    //공격력
            this.hp = 10;     //체력
            this.exp = 3;    //경험치 계수
        }
    }
}
