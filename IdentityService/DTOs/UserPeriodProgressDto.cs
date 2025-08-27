using System;
namespace VemboAPI.Domain.DTOs
{
	public class UserPeriodProgressDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int PeriodId { get; set; }
		public int XP { get; set; }
		public int CompletedCount { get; set; }
	}
}

