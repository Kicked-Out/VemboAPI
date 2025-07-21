namespace VemboAPI.Domain.DTOs
{
    public class CreateUserTopicProgressDto
    {
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public bool isCompleted { get; set; }
    }
}
