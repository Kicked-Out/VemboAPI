using System;

namespace VemboAPI.Domain.Entities
{
    public class UserStreak
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CurrentStreak { get; set; } = 0;
        public int LongestStreak { get; set; } = 0;
        public DateTime? LastActiveDate { get; set; }
        public int StreakFreezes { get; set; } = 0;
        public User? User { get; set; }
    }
}
