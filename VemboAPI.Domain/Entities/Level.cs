using System;
namespace VemboAPI.Domain.Entities
{
	public class Level
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int UnitId { get; set; }
		public int Order { get; set; }
		public Unit? Unit { get; set; } 
	}
}

