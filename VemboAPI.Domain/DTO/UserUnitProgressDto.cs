using System;
namespace VemboAPI.Domain.DTOs
{
	public class UserUnitProgressDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int UnitId { get; set; }
		public bool isCompleted { get; set; }
	}
}

