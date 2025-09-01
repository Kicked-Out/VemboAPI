using System.Collections.Generic;

namespace VemboAPI.Domain.Entities
{
    public class Medal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public ICollection<UserMedal> UserMedals { get; set; } = new List<UserMedal>();
    }
}
