using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IDailyQuestService
    {
        List<DailyQuestDto> GetAll();
        DailyQuestDto GetById(int id);
        DailyQuestDto Create(CreateDailyQuestDto dto);
        void Update(int id, UpdateDailyQuestDto dto);
        void Delete(int id);
    }
}
