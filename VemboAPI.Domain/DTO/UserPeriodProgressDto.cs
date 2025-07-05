using System;
namespace VemboAPI.Domain.DTOs
{
	public class UserPeriodProgressDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int PeriodId { get; set; }
		public bool isCompleted { get; set; }
	}
}

