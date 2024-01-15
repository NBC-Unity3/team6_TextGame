using Newtonsoft.Json;
using team6_TextGame.Characters;
using team6_TextGame.Items;

namespace team6_TextGame
{
    internal class QuestBoard
    {
        public List<Quest> quests = new List<Quest>();

        public void AddQuest(Quest quest)
        {
            quests.Add(quest);
        }

        public void RemoveQuest(Quest quest)
        {
            quests.Remove(quest);
        }

        public void ClearCheck(Character character)
        {
            foreach (Quest quest in quests)
            {
                if(quest.name == "더욱 더 강해지기!")
                {
                    quest.achieve_count = character.f_atk;
                }
                if (quest.achieve_count >= quest.goal_count && quest.isActive == true)
                {
                    quest.isClear = true;
                }
            }
        }
        public void ReceiveReward(Quest quest, Player character)
        {
            if(quest.name == "더욱 더 강해지기!")
            {
                Quest nextQuest = quests.Find(element => element.name == "더욱 더 강해지기! (2)");
                nextQuest.isAvailable = true;
            }
            quests.Remove(quest);
            character.gold += quest.gold_reward;
            foreach (EquipItem item in quest.item_rewards)
            {
                character.equips.Add(item);
            }
            for (int i = 0; i < 3; i++)
            {
                if (quest.item_rewards2[i] != 0)
                {
                    if (i == 0)
                    {
                        //AtkPotion atkPotion = new AtkPotion();
                        //atkPotion.count = quest.item_rewards2[i];
                        //character.AddConsumesInven(atkPotion);
                    }
                    else if (i == 1)
                    {
                        //DefPotion defPotion = new DefPotion();
                        //defPotion.count = quest.item_rewards2[i];
                        //character.AddConsumesInven(defPotion);
                    }
                    else if (i == 2)
                    {
                        //HpPotion hpPotion = new HpPotion();
                        //hpPotion.count = quest.item_rewards2[i];
                        //character.AddConsumesInven(hpPotion);
                    }
                }
            }
            /*
            foreach (var item in quest.item_rewards2)
            {
                character.consumes.Add(item);
            }*/
        }

        public void SaveOptions()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/questboard.json";
            string json = JsonConvert.SerializeObject(quests, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public void LoadOptions()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/questboard.json";
            if (!File.Exists(path)) SaveOptions();

            string json = File.ReadAllText(path);

            List<Quest> quests = JsonConvert.DeserializeObject<List<Quest>>(json);

            this.quests = quests;
        }
    }
}
