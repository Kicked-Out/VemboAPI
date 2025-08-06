using System;
namespace VemboAPI.Domain.DTOs
{
    public class CreateAchievementLevelDto
    {
        public int AchievementId { get; set; }
        public int Level { get; set; }
        public int TargetValue { get; set; }
        public int RewardXP { get; set; }
    }
}

