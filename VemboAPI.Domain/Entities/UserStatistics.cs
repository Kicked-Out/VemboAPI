using System;
namespace VemboAPI.Domain.Entities
{
    public class UserStatistic
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int Streak { get; set; } = 0;
        public int Emeralds { get; set; } = 0;
        public int Hearts { get; set; } = 5;

        public int CurrentPeriodId { get; set; }
        public Period CurrentPeriod { get; set; }
    }

}

