// UnitDto.cs
namespace VemboAPI.Domain.DTOs
{
    public class UnitDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int TopicId { get; set; }
    }
}
