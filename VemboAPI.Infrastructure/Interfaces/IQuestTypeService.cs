using System.Collections.Generic;
using System.Threading.Tasks;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IQuestTypeService
    {
        Task<List<QuestTypeDto>> GetAllAsync();
        Task<QuestTypeDto> GetByIdAsync(int id);
        Task<QuestTypeDto> CreateAsync(CreateQuestTypeDto dto);
        Task UpdateAsync(int id, UpdateQuestTypeDto dto);
        Task DeleteAsync(int id);
    }
}
