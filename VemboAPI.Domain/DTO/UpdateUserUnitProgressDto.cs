namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserUnitProgressDto
    {
        public string UserId { get; set; }
        public int UnitId { get; set; }
        public int CompletedCount { get; set; }
    }
}
