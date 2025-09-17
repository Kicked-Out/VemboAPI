namespace VemboAPI.Domain.DTOs
{
    public class CreateUserUnitProgressDto
    {
        public string UserId { get; set; }
        public int UnitId { get; set; }
        public int CompletedCount { get; set; }
    }
}
