using System.Text;
using team6_TextGame.Characters;


namespace team6_TextGame.Items
{
    internal class EquipItem : Item
    {
        public bool isEquipped { get; set; } = false;

        public EquipItem(int id, string name, int atk, int def, int hp, string info, int price)
        {
            this.id = id;
            this.name = name;
            this.atk = atk;
            this.def = def;
            this.hp = hp;
            this.info = info;
            this.price = price;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (isEquipped) { sb.Append("[E]"); }
            sb.Append(name);
            sb.Append(" | ");
            if (atk != 0) { sb.Append("공격력 + " + atk + " "); }
            if (def != 0) { sb.Append("방어력 + " + def + " "); }
            if (hp != 0) { sb.Append("체력 + " + hp + " "); }
            sb.Append("| ");
            sb.Append(info);

            return sb.ToString();
        }

        public void equip(Player player)
        {
            if (isEquipped)
            {
                if (type == 1)
                {
                    isEquipped = false;
                    player.weaponEquip = false;
                    player.ChangeStatus(-atk, -def, -hp);
                }
                else if (type == 2)
                {
                    isEquipped = false;
                    player.armorEquip = false;
                    player.ChangeStatus(-atk, -def, -hp);
                }
            }
            else
            {
                if (type == 1)
                {
                    if (player.weaponEquip == true)
                    {
                        for (int i = 0; i < player.equips.Count; i++)
                        {
                            if (player.equips[i].isEquipped == true && player.equips[i].type == 1)
                            {
                                player.equips[i].isEquipped = false;
                                player.ChangeStatus(-player.equips[i].atk, -player.equips[i].def, -player.equips[i].hp);
                            }
                        }
                        isEquipped = true;
                        player.ChangeStatus(atk, def, hp);
                    }
                    else
                    {
                        isEquipped = true;
                        player.weaponEquip = true;
                        player.ChangeStatus(atk, def, hp);
                    }

                }
                else if (type == 2)
                {
                    if (player.armorEquip == true)
                    {
                        for (int i = 0; i < player.equips.Count; i++)
                        {
                            if (player.equips[i].isEquipped == true && player.equips[i].type == 2)
                            {
                                player.equips[i].isEquipped = false;
                                player.ChangeStatus(-player.equips[i].atk, -player.equips[i].def, -player.equips[i].hp);
                            }
                        }
                        isEquipped = true;
                        player.ChangeStatus(atk, def, hp);
                    }
                    else
                    {
                        isEquipped = true;
                        player.armorEquip = true;
                        player.ChangeStatus(atk, def, hp);
                    }
                }
            }
        }
    }
}
