using System;
namespace VemboAPI.Domain.Entities
{
	public class Question
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int ExerciseId { get; set; }
		public Exercise? Exercise { get; set; }
		public ICollection<Answer> Answers { get; set; } = new List<Answer>();
	}
}

