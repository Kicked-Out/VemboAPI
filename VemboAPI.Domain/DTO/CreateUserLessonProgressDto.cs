namespace VemboAPI.Domain.DTOs
{
    public class CreateUserLessonProgressDto
    {
        public int UserId { get; set; }
        public int LessonId { get; set; }
        public bool isCompleted { get; set; }
    }
}
