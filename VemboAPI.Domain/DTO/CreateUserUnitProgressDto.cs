namespace VemboAPI.Domain.DTOs
{
    public class CreateUserUnitProgressDto
    {
        public int UserId { get; set; }
        public int UnitId { get; set; }
        public bool isCompleted { get; set; }
    }
}
