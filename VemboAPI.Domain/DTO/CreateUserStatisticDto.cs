using System;
namespace VemboAPI.Domain.DTOs
{
    public class CreateUserStatisticDto
    {
        public int UserId { get; set; }
        public int Streak { get; set; } = 0;
        public int Emeralds { get; set; } = 0;
        public int Hearts { get; set; } = 5;
        public int CurrentPeriodId { get; set; }
    }
}

