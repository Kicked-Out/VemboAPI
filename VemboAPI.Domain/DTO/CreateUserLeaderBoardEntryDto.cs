using System;
namespace VemboAPI.Domain.DTOs
{
    public class CreateUserLeaderBoardEntryDto
    {
        public string UserId { get; set; }
        public int TotalXP { get; set; } = 0;
        public int Rank { get; set; } = 0;
    }
}

