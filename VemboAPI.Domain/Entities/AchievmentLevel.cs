using System;
namespace VemboAPI.Domain.Entities
{
    public class AchievementLevel
    {
        public int Id { get; set; }
        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }

        public int Level { get; set; }
        public int TargetValue { get; set; }
        public int RewardXP { get; set; }
    }

}

