namespace VemboAPI.Domain.DTOs
{
    public class CreateAnswerDto
    {
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
