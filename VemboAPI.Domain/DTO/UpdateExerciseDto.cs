namespace VemboAPI.Domain.DTOs
{
    public class UpdateExerciseDto
    {
        public string Title { get; set; }
        public int LessonId { get; set; }
        public bool Difficulty { get; set; }
        public int ExerciseTypeId { get; set; }
        public int Order { get; set; }
    }
}
