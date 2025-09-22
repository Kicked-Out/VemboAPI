namespace VemboAPI.Domain.Entities
{
    public class UserQuestProgress
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int QuestId { get; set; }
        public Quest Quest { get; set; }
        public int Progress { get; set; }
        public bool IsCompleted { get; set; }
    }
}
