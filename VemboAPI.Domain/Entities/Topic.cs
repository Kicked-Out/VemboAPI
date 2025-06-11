using System;
using System.Collections.Generic;

namespace VemboAPI.Domain.Entities
{
    public class Topic
    {
        public int Id { get; set; }


        public string Title { get; set; }


        public string Description { get; set; }


        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
