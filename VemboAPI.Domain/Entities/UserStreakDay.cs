using System;

namespace VemboAPI.Domain.Entities
{
    public class UserStreakDay
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public User? User { get; set; }
    }
}
