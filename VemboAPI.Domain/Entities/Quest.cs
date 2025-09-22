using System;
using System.Collections.Generic;

namespace VemboAPI.Domain.Entities
{
    public class Quest
    {
        public int Id { get; set; }
        public int QuestDefinitionId { get; set; }
        public QuestDefinition QuestDefinition { get; set; }
        public int QuestTypeId { get; set; }
        public QuestType QuestType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? MedalId { get; set; }

        public ICollection<UserQuestProgress> UserQuestProgresses { get; set; } = new List<UserQuestProgress>();
    }
}
