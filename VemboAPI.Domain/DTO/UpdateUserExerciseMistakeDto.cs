namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserExerciseMistakeDto
    {
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
        public string UserAnswer { get; set; }
    }
}
