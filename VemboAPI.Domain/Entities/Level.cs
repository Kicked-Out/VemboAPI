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
		public int LevelTypeId { get; set; }
		public LevelType? LevelType { get; set; }
		public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
		public ICollection<UserLevelProgress> UserLevelProgresses { get; set; } = new List<UserLevelProgress>();
	}
}

