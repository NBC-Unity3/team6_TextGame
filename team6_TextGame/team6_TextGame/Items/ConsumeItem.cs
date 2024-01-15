
using System.Text;
using team6_TextGame.Characters;

namespace team6_TextGame.Items
{
    internal class ConsumeItem : Item
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(name);
            sb.Append(" | ");
            if (atk != 0) { sb.Append("공격력 + " + atk + " "); }
            if (def != 0) { sb.Append("방어력 + " + def + " "); }
            if (hp != 0) { sb.Append("체력 + " + hp + " "); }
            sb.Append("| ");
            sb.Append(info);

            return sb.ToString();
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
                if (player.hp > 100)
                    player.hp = 100;
                Console.WriteLine("체력이 " + hp + " 상승하였습니다.");
            }
        }
    }
}
