using System;
namespace VemboAPI.Domain.Entities
{
    public class UserLevelProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LevelId { get; set; }
        public bool isCompleted { get; set; }
        public User? User { get; set; }
        public Level? Level { get; set; }
    }
}

