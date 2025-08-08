using System;
namespace VemboAPI.Domain.DTOs
{
    public class UserLeaderBoardEntryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int XP { get; set; }
        public int Rank { get; set; }
    }
}

