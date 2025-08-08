namespace VemboAPI.Domain.DTOs
{
    public class CreateUserExerciseMistakeDto
    {
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
        public string UserAnswer { get; set; }
    }
}
