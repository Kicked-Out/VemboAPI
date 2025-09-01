using System;
namespace VemboAPI.Domain.Entities
{
	public class UserUnitProgress
	{
		public int Id { get; set; }
		public int UnitId { get; set; }
		public string UserId { get; set; }
		public int CompletedCount { get; set; }
		public User? User { get; set; }
		public Unit? Unit { get; set; }
	}
}