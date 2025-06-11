using System;

namespace VemboAPI.Domain.Entities
{
    public class Part
    {
        public int Id { get; set; }

      
        public string Title { get; set; }


        public int TopicId { get; set; }

    
        public Topic? Topic { get; set; }
    }
}
