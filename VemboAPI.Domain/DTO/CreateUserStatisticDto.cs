using System;
namespace VemboAPI.Domain.DTOs
{
    public class CreateUserStatisticDto
    {
        public string UserId { get; set; }
        public int Streak { get; set; } = 0;
        public int VBucks { get; set; } = 0;
        public int Hearts { get; set; } = 5;
        public int CurrentPeriodId { get; set; }
        public int CurrentLevelId { get; set; }
    }
}

