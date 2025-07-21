namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserLevelProgressDto
    {
        public int UserId { get; set; }
        public int LevelId { get; set; }
        public bool isCompleted { get; set; }
    }
}
