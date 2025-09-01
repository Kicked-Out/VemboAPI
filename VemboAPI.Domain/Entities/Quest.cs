using System.Collections.Generic;

namespace VemboAPI.Domain.Entities
{
    public enum QuestType
    {
        Daily,
        Monthly
    }

    public class Quest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestType QuestType { get; set; }
        public int Requirement { get; set; }
        public string RewardType { get; set; }
        public int RewardAmount { get; set; }

        public ICollection<DailyQuest> DailyQuests { get; set; } = new List<DailyQuest>();
        public ICollection<UserQuest> UserQuests { get; set; } = new List<UserQuest>();
    }
}
