using System.Collections.Generic;

namespace VemboAPI.Domain.Entities
{
    public class QuestType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<Quest> Quests { get; set; } = new List<Quest>();
    }
}
