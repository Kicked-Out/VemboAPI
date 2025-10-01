namespace VemboAPI.Domain.Entities
{
    public class QuestDefinition
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequirementType { get; set; }
        public int Requirement { get; set; }
        public string RewardType { get; set; }
        public int RewardAmount { get; set; }

        public ICollection<Quest> Quests { get; set; } = new List<Quest>();
    }
}
