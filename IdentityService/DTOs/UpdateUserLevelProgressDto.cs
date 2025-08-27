namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserLevelProgressDto
    {
        public string UserId { get; set; }
        public int LevelId { get; set; }
        public int CompletedCount { get; set; }
    }
}
