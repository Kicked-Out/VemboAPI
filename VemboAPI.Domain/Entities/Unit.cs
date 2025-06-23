using System;

namespace VemboAPI.Domain.Entities
{
    public class Unit
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        public int TopicId { get; set; }

        public Topic? Topic { get; set; }
    }
}
