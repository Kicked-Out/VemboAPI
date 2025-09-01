using System;

namespace VemboAPI.Domain.DTOs
{
    public class UserMedalDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MedalId { get; set; }
        public DateTime AwardedAt { get; set; }
    }
}
