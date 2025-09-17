namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserExerciseMistakeDto
    {
        public string UserId { get; set; }
        public int ExerciseId { get; set; }
        public string UserAnswer { get; set; }
    }
}
