using System;
namespace VemboAPI.Domain.DTOs
{
    public class AchievementDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
    }
}

