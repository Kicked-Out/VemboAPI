namespace VemboAPI.Domain.DTOs
{
    public class UpdateMedalDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
