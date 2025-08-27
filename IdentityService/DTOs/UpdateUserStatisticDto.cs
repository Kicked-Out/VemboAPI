using System;
namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserStatisticDto
    {
        public int Streak { get; set; }
        public int VBucks { get; set; }
        public int Hearts { get; set; }
        public int CurrentPeriodId { get; set; }
        public int CurrentLevelId { get; set; }
    }
}

