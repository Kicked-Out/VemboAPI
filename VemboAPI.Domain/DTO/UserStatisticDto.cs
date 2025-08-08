using System;
namespace VemboAPI.Domain.DTOs
{
    public class UserStatisticDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Streak { get; set; }
        public int Emeralds { get; set; }
        public int Hearts { get; set; }
        public int? CurrentPeriodId { get; set; }
    }
}

