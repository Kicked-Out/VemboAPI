using System;

namespace VemboAPI.Domain.Entities
{
    public class UserBadge
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int BadgeId { get; set; }
        public Badge Badge { get; set; }
    }
}
