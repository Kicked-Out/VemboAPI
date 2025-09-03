using System;

namespace VemboAPI.Domain.DTOs
{
    public class CreateUserStreakDayDto
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
