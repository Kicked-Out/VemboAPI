using System;

namespace VemboAPI.Domain.DTOs
{
    public class CreateUserStreakDto
    {
        public string UserId { get; set; }
        public int CurrentStreak { get; set; } = 0;
        public int LongestStreak { get; set; } = 0;
        public DateTime? LastActiveDate { get; set; }
        public int StreakFreezes { get; set; } = 0;
    }
}
