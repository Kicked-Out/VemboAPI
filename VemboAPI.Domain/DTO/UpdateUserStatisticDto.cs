using System;
namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserStatisticDto
    {
        public int Streak { get; set; }
        public int Emeralds { get; set; }
        public int Hearts { get; set; }
        public int CurrentPeriodId { get; set; }
    }
}

