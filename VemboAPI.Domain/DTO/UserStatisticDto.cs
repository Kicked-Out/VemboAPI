using System;
namespace VemboAPI.Domain.DTOs
{
    public class UserStatisticDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Streak { get; set; }
        public int VBucks { get; set; }
        public int Hearts { get; set; }
        public int TotalXP { get; set; }
        public int CurrentPeriodId { get; set; }
        public int CurrentLevelId { get; set; }
    }
}

