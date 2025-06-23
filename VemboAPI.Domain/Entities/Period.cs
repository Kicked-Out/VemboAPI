using System;
using System.Collections.Generic;

namespace VemboAPI.Domain.Entities
{
	public class Period
	{
		public int Id { get; set; }
		public string Title { get; set; }
        public string Description { get; set; }
		public string ImageUrl { get; set; }
        public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    }
}

