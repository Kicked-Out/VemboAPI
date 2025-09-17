using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IQuestService
    {
        List<QuestDto> GetAll();
        QuestDto GetById(int id);
        QuestDto Create(CreateQuestDto dto);
        void Update(int id, UpdateQuestDto dto);
        void Delete(int id);
    }
}
