using System;
namespace VemboAPI.Domain.Entities
{
    public class UserAchievement
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }

        public int CurrentLevel { get; set; } = 1;
        public int Progress { get; set; } = 0;
        public DateTime EarnedAt { get; set; }
        public bool IsCompleted { get; set; } = false;
    }

}

