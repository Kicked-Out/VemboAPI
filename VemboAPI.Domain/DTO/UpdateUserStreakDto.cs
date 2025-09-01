using System;

namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserStreakDto
    {
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public int StreakFreezes { get; set; }
    }
}
