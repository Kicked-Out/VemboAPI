namespace VemboAPI.Domain.Entities
{
    public class UserQuest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestId { get; set; }
        public Quest Quest { get; set; }
        public int Progress { get; set; } = 0;
        public bool IsCompleted { get; set; } = false;
    }
}
