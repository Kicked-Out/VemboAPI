using System;
namespace VemboAPI.Domain.DTOs
{
    public class UserAchievementDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AchievementId { get; set; }
        public int CurrentLevel { get; set; }
        public int Progress { get; set; }
        public DateTime EarnedAt { get; set; }
        public bool IsCompleted { get; set; }
    }
}

