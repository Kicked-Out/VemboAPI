using System;
namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserAchievementDto
    {
        public int CurrentLevel { get; set; }
        public int Progress { get; set; }
        public DateTime EarnedAt { get; set; }
        public bool IsCompleted { get; set; }
    }
}

