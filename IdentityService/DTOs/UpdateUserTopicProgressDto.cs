namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserTopicProgressDto
    {
        public string UserId { get; set; }
        public int TopicId { get; set; }
        public int CompletedCount { get; set; }
    }
}
