using System;

namespace VemboAPI.Domain.DTOs
{
    public class UserBadgeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BadgeId { get; set; }
        public DateTime EarnedAt { get; set; }
    }
}
