namespace VemboAPI.Domain.DTOs
{
    public class UserQuestProgressDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int QuestId { get; set; }
        public int Progress { get; set; }
        public bool IsCompleted { get; set; }
    }
}
