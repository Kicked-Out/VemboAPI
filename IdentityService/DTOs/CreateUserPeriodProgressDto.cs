namespace VemboAPI.Domain.DTOs
{
    public class CreateUserPeriodProgressDto
    {
        public string UserId { get; set; }
        public int PeriodId { get; set; }
        public int XP { get; set; }
        public int CompletedCount { get; set; }
    }
}
