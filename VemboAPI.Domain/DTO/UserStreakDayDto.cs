using System;

namespace VemboAPI.Domain.DTOs
{
    public class UserStreakDayDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
