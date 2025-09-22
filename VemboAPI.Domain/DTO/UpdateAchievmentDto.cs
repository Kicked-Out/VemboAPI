using Microsoft.AspNetCore.Http;
using System;

namespace VemboAPI.Domain.DTOs
{
    public class UpdateAchievementDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string CompletedIconUrl { get; set; }
    }
}
