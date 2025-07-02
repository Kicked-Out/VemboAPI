using System;
namespace VemboAPI.Domain.DTOs
{
	public class ExerciseDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int LessonId { get; set; }
		public bool Difficulty { get; set; }
		public int ExerciseTypeId { get; set; }
		public int Order { get; set; }
	}
}

