namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserTopicProgressDto
    {
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public bool isCompleted { get; set; }
    }
}
