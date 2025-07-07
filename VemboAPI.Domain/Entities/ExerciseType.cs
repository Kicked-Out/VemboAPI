using System;
namespace VemboAPI.Domain.Entities
{
	public class ExerciseType
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
	}
}

