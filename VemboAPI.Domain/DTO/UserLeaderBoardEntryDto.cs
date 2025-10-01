using System;
namespace VemboAPI.Domain.DTOs
{
    public class UserLeaderBoardEntryDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TotalXP { get; set; }
        public int Rank { get; set; }
    }
}

