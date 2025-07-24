namespace VemboAPI.Domain.DTOs
{
    public class UpdateUnitDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int TopicId { get; set; }
        public int GuideBookId { get; set; }
    }
}