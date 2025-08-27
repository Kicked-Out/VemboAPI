namespace VemboAPI.Domain.DTOs
{
    public class CreateUserLevelProgressDto
    {
        public string UserId { get; set; }
        public int LevelId { get; set; }
        public int CompletedCount { get; set; }
    }
}
