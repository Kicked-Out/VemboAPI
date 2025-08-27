using System;
namespace VemboAPI.Domain.Entities
{
    public class UserLeaderBoardEntry
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int XP { get; set; } = 0;
        public int Rank { get; set; } = 0;
    }

}

