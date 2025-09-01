namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserLessonProgressDto
    {
        public string UserId { get; set; }
        public int LessonId { get; set; }
        public int CompletedCount { get; set; }
    }
}
