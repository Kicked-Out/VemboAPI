namespace VemboAPI.Domain.Entities
{
    public class UserQuest
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int QuestDefinitionId { get; set; }
        public QuestDefinition QuestDefinition { get; set; }
        public int Progress { get; set; } = 0;
        public bool IsCompleted { get; set; } = false;
    }
}
