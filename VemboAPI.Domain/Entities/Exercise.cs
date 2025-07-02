using System;
namespace VemboAPI.Domain.Entities
{
	public class Exercise
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int LessonId { get; set; }
		public bool Difficulty { get; set; }
		public int Order { get; set; }
		public int ExerciseTypeId { get; set; }
		public Lesson? Lesson { get; set; }
		public ExerciseType? ExerciseType { get; set; }
	}
}

