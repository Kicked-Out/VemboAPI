using System;

namespace VemboAPI.Domain.DTOs
{
    public class UpdateDailyQuestDto
    {
        public int QuestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? MedalId { get; set; }
    }
}
