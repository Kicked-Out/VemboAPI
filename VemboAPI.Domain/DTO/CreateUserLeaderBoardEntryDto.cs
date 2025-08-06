using System;
namespace VemboAPI.Domain.DTOs
{
    public class CreateUserLeaderBoardEntryDto
    {
        public int UserId { get; set; }
        public int XP { get; set; } = 0;
        public int Rank { get; set; } = 0;
    }
}

