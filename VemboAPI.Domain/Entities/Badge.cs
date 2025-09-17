using System;
using System.Collections.Generic;

namespace VemboAPI.Domain.Entities
{
    public class Badge
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string Month { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
    }
}
