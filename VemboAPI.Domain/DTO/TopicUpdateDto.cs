namespace VemboAPI.Domain.DTOs
{
    public class TopicUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int PeriodId { get; set; }
    }
}