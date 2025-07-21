using System;
namespace VemboAPI.Domain.Entities
{
	public class UserUnitProgress
	{
		public int Id { get; set; }
		public int UnitId { get; set; }
		public int UserId { get; set; }
		public bool isCompleted { get; set; }
		public User? User { get; set; }
		public Unit? Unit { get; set; }
	}
}

