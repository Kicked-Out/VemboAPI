using System;

namespace VemboAPI.Domain.DTOs
{
    public class CreateUserMedalDto
    {
        public string UserId { get; set; }
        public int MedalId { get; set; }
        public DateTime AwardedAt { get; set; } = DateTime.UtcNow;
    }
}
