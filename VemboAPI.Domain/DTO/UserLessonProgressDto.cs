using System;
namespace VemboAPI.Domain.DTOs
{
	public class UserLessonProgressDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int LessonId { get; set; }
		public bool isCompleted { get; set; }
	}
}

