using System.Collections.Generic;

namespace VemboAPI.Domain.Entities
{
    public enum QuestDefinitionCategory
    {
        Daily,
        Monthly
    }

    public class QuestDefinition
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestDefinitionCategory Category { get; set; }
        public int Requirement { get; set; }
        public string RewardType { get; set; }
        public int RewardAmount { get; set; }

        public ICollection<Quest> Quests { get; set; } = new List<Quest>();
        public ICollection<UserQuest> UserQuests { get; set; } = new List<UserQuest>();
    }
}
