namespace VemboAPI.Domain.DTOs
{
    public class CreateQuestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string QuestType { get; set; }
        public int Requirement { get; set; }
        public string RewardType { get; set; }
        public int RewardAmount { get; set; }
    }
}
