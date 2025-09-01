namespace VemboAPI.Domain.DTOs
{
    public class CreateUserLessonProgressDto
    {
        public string UserId { get; set; }
        public int LessonId { get; set; }
        public int CompletedCount { get; set; }
    }
}
