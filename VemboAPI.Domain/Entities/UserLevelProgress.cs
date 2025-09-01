using System;
namespace VemboAPI.Domain.Entities
{
    public class UserLevelProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int LevelId { get; set; }
        public int CompletedCount { get; set; }
        public User? User { get; set; }
        public Level? Level { get; set; }
    }
}

