namespace VemboAPI.Domain.DTOs
{
    public class CreateUserPeriodProgressDto
    {
        public int UserId { get; set; }
        public int PeriodId { get; set; }
        public bool isCompleted { get; set; }
    }
}
