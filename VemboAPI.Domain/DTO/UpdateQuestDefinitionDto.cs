namespace VemboAPI.Domain.DTOs
{
    public class UpdateQuestDefinitionDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequirementType { get; set; }
        public int Requirement { get; set; }
        public string RewardType { get; set; }
        public int RewardAmount { get; set; }
    }
}
