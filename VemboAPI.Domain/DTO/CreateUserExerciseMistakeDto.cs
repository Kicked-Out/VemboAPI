namespace VemboAPI.Domain.DTOs
{
    public class CreateUserExerciseMistakeDto
    {
        public string UserId { get; set; }
        public int ExerciseId { get; set; }
        public string UserAnswer { get; set; }
    }
}
