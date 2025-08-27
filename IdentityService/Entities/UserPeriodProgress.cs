using System;
namespace VemboAPI.Domain.Entities
{
	public class UserPeriodProgress
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int PeriodId { get; set; }
		public int XP { get; set; }
		public int CompletedCount { get; set; }
		public User? User { get; set; }
		public Period? Period { get; set; }
	}
}

