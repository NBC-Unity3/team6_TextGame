

namespace team6_TextGame.Monsters
{
    internal class CanonMinion : Monster
    {
        public static int GetMinimumLevel()
        {
            return 3; // 등장할 수 있는 최소 레벨
        }
        public CanonMinion()
        {
            this.name = "대포 미니언";   //이름
            this.level = 5;  //레벨
            this.atk = 8;    //공격력
            this.hp = 25;     //체력
            this.exp = 5;    //경험치 계수
        }
    }
}
