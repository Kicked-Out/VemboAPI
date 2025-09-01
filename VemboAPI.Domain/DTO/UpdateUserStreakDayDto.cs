using System;

namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserStreakDayDto
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
