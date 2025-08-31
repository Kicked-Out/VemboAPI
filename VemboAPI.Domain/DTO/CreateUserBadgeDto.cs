using System;

namespace VemboAPI.Domain.DTOs
{
    public class CreateUserBadgeDto
    {
        public int UserId { get; set; }
        public int BadgeId { get; set; }
        public DateTime EarnedAt { get; set; }
    }
}
