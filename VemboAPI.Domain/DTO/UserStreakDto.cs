using System;

namespace VemboAPI.Domain.DTOs
{
    public class UserStreakDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public int StreakFreezes { get; set; }
    }
}
