using System;
namespace VemboAPI.Domain.DTOs
{
	public class UserUnitProgressDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int UnitId { get; set; }
		public int CompletedCount { get; set; }
	}
}

