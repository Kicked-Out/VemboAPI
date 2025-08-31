using System;

namespace VemboAPI.Domain.Entities
{
    public class DailyQuest
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public Quest Quest { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? MedalId { get; set; }
    }
}
