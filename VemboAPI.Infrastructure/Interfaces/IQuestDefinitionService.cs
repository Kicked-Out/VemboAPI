using System.Collections.Generic;
using System.Threading.Tasks;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IQuestDefinitionService
    {
        Task<List<QuestDefinitionDto>> GetAllAsync();
        Task<QuestDefinitionDto> GetByIdAsync(int id);
        Task<QuestDefinitionDto> CreateAsync(CreateQuestDefinitionDto dto);
        Task UpdateAsync(int id, UpdateQuestDefinitionDto dto);
        Task DeleteAsync(int id);
    }
}
