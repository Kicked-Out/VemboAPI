using System;

namespace VemboAPI.Domain.DTOs
{
    public class CreateQuestDto
    {
        public int QuestDefinitionId { get; set; }
        public int QuestTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? MedalId { get; set; }
    }
}
