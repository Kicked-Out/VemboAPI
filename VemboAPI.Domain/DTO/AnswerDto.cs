using System;
namespace VemboAPI.Domain.DTOs
{
	public class AnswerDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public bool isCorrect { get; set; }
		public int QuestionId { get; set; }

	}
}

