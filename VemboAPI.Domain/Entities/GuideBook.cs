using System;
namespace VemboAPI.Domain.Entities
{
	public class GuideBook
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Content { get; set; }
		public ICollection<Unit> Units { get; set; } = new List<Unit>();
	}
}

