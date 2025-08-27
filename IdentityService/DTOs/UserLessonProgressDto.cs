using System;
namespace VemboAPI.Domain.DTOs
{
	public class UserLessonProgressDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int LessonId { get; set; }
		public int CompletedCount { get; set; }
	}
}

