using System.Collections.Generic;
using System.Threading.Tasks;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IQuestService
    {
        Task<List<QuestDto>> GetAllAsync();
        Task<QuestDto> GetByIdAsync(int id);
        Task<QuestDto> CreateAsync(CreateQuestDto dto);
        Task UpdateAsync(int id, UpdateQuestDto dto);
        Task DeleteAsync(int id);
    }
}
