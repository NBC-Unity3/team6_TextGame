using Newtonsoft.Json;
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

        public void QuestClear(Quest quest, Player character)
        {
            quests.Remove(quest);
            character.gold += quest.gold_reward;
            foreach (EquipItem item in quest.item_rewards) 
            {
                character.AddInventory(item);
            }
            SaveOptions();
        }
        public void RemoveQuest(Quest quest)
        {
            quests.Remove(quest);
            SaveOptions();
        }

        public void ClearCheck()
        {
            foreach (Quest quest in quests)
            {
                if (quest.achieve_count >= quest.goal_count)
                {
                    quest.isClear = true;
                }
            }
            SaveOptions();
        }
        public void ReceiveReward(Quest quest, Player character)
        {
            quests.Remove(quest);
            character.gold += quest.gold_reward;
            foreach (EquipItem item in quest.item_rewards)
            {
                character.equips.Add(item);
            }
            foreach (ConsumeItem item in quest.item_rewards2)
            {
                character.consumes.Add(item);
            }
            SaveOptions();
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
