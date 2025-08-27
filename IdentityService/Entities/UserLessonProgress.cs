using System;
namespace VemboAPI.Domain.Entities
{
	public class UserLessonProgress
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int LessonId { get; set; }
		public int CompletedCount { get; set; }
		public User? User { get; set; }
		public Lesson? Lesson { get; set; }
	}
}

