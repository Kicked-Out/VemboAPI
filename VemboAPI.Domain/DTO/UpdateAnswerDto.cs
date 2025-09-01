namespace VemboAPI.Domain.DTOs
{
    public class UpdateAnswerDto
    {
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
