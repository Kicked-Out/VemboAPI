namespace VemboAPI.Domain.DTOs
{
    public class UserQuestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestId { get; set; }
        public int Progress { get; set; }
        public bool IsCompleted { get; set; }
    }
}
