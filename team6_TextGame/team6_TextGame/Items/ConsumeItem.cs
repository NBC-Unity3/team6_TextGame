
using System.Text;

namespace team6_TextGame.Items
{
    internal class ConsumeItem : Item
    {
        public int count = 0;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(name);
            sb.Append(" | ");
            sb.Append(info);
            sb.Append("| ");
            sb.Append(count);
            sb.Append("개");

            return sb.ToString();
        }

        public void ShowState()
        {
            if (count > 0)
                Console.WriteLine(ToString());
            else
                return;
        }

        public void Consume(Player player)
        {
            if (atk > 0)
            {
                player.atk += atk;
                Console.WriteLine("공격력이 " + atk + " 상승하였습니다.");
            }
            else if (def > 0)
            {
                player.def += def;
                Console.WriteLine("방어력이 " + def + " 상승하였습니다.");
            }
            else if (hp > 0)
            {
                player.hp += hp;
                Console.WriteLine("체력이 " + hp + " 상승하였습니다.");
            }
        }
    }
}
