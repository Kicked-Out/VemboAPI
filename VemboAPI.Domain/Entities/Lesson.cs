using System;
namespace VemboAPI.Domain.Entities
{
	public class Lesson
	{
		public int Id { get; set; }
		public int Order { get; set; }
		public int LevelId { get; set; }
		public Level? Level { get; set; }
	}
}

