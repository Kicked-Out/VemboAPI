using System;
namespace VemboAPI.Domain.DTOs
{
	public class UserLevelProgressDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int LevelId { get; set; }
		public bool isCompleted { get; set; }
	}
}

