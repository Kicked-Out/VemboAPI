using System;
namespace VemboAPI.Domain.DTOs
{
	public class UserLevelProgressDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int LevelId { get; set; }
		public int CompletedCount { get; set; }
	}
}

