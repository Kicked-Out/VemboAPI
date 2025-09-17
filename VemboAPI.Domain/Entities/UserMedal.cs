using System;

namespace VemboAPI.Domain.Entities
{
    public class UserMedal
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int MedalId { get; set; }
        public Medal Medal { get; set; }

        public DateTime AwardedAt { get; set; } = DateTime.UtcNow;
    }
}
