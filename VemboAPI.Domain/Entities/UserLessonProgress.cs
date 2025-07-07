using System;
namespace VemboAPI.Domain.Entities
{
	public class UserLessonProgress
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int LessonId { get; set; }
		public bool isCompleted { get; set; }
		public User? User { get; set; }
		public Lesson? Lesson { get; set; }
	}
}

