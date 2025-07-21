namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserUnitProgressDto
    {
        public int UserId { get; set; }
        public int UnitId { get; set; }
        public bool isCompleted { get; set; }
    }
}
