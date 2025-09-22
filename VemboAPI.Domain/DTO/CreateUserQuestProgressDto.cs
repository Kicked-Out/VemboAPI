namespace VemboAPI.Domain.DTOs
{
    public class CreateUserQuestProgressDto
    {
        public string UserId { get; set; }
        public int QuestId { get; set; }
        public int Progress { get; set; }
        public bool IsCompleted { get; set; }
    }
}
