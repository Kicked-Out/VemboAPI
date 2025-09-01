using System;

namespace VemboAPI.Domain.DTOs
{
    public class UpdateBadgeDto
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
    }
}
