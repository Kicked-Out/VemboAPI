using System;
namespace VemboAPI.Domain.Entities
{
	public class LevelType
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public ICollection<Level> Levels { get; set; } = new List<Level>();
	}
}

