using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VemboAPI.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Level { get; set; }
        public int Rating { get; set; }
        public bool IsPremium { get; set; }
        public long XP { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<UserPeriodProgress> UserPeriodProgresses { get; set; } = new List<UserPeriodProgress>();
        public ICollection<UserTopicProgress> UserTopicProgresses { get; set; } = new List<UserTopicProgress>();
        public ICollection<UserUnitProgress> UserUnitProgresses { get; set; } = new List<UserUnitProgress>();
        public ICollection<UserLevelProgress> UserLevelProgresses { get; set; } = new List<UserLevelProgress>();
        public ICollection<UserLessonProgress> UserLessonProgresses { get; set; } = new List<UserLessonProgress>();
        public ICollection<UserExerciseMistake> UserExerciseMistakes { get; set; } = new List<UserExerciseMistake>();

        public string Role { get; set; } = "User";
    }
}
