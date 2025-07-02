using System;
using System.Collections.Generic;

namespace VemboAPI.Domain.Entities
{
    public class Topic
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

       
        public int PeriodId { get; set; }

        public Period? Period { get; set; }

        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
