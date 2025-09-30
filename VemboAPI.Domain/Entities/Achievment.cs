using System;
using System.Collections.Generic;

namespace VemboAPI.Domain.Entities
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TargetType { get; set; }
        public string IconUrl { get; set; }
        public string CompletedIconUrl { get; set; }

        public ICollection<AchievementLevel> Levels { get; set; } = new List<AchievementLevel>();
        public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
    }
}
