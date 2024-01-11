﻿using team6_TextGame.Items;

namespace team6_TextGame
{
    internal class Quest
    {
        public string name { get; set; }   // 이름
        public string explain { get; set; } // 퀘스트 설명
        public string goal_description { get; set; } // 목표 설명
        public int achieve_count { get; set; } // 달성 횟수
        public int goal_count { get; set; } // 필요 달성 횟수
        public int gold_reward { get; set; } // 보상 골드
        public List<EquipItem> item_rewards = new List<EquipItem>(); // 보상 아이템
        public List<ConsumeItem> item_rewards2 = new List<ConsumeItem>(); // 보상 아이템
        public bool isActive { get; set; } = false;
        public bool isClear { get; set; } = false;

        public override string ToString()
        {
            string str = "";
            str += name + "\n\n";
            str += explain + "\n- ";
            str += goal_description + "(" + achieve_count + "/" + goal_count + ")\n\n- ";
            str += "보상\n";
            foreach (EquipItem item in item_rewards)
            {
                str += item.name + "x1\n";
            }
            foreach (ConsumeItem item in item_rewards2)
            {
                str += item.name + "x" + item.count + "\n";
            }
            str += gold_reward + "G\n";

            return str;
        }
    }
}
