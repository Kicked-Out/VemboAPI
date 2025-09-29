using System;

namespace VemboAPI.Domain.DTOs
{
    public class BlockUserDto
    {
        public DateTimeOffset? LockedUntil { get; set; }
        public string? Reason { get; set; }
    }
}
