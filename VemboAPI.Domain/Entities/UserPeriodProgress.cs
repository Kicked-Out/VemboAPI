using System;
namespace VemboAPI.Domain.Entities
{
	public class UserPeriodProgress
	{
		public int Id { get; set; }
		public bool isCompleted { get; set; }
		public int UserId { get; set; }
		public int PeriodId { get; set; }
		public User? User { get; set; }
		public Period? Period { get; set; }
	}
}

