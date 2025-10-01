using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IQuestService
    {
        Task<List<QuestDto>> GetAllAsync();
        Task<QuestDto> GetByIdAsync(int id);
        Task<List<QuestDto>> GetAllMonthly();
        Task<QuestDto> GetCurrentMonthly();
        Task<List<QuestDto>> GetCurrentDaily();
        Task<QuestDto> CreateAsync(CreateQuestDto dto);
        Task UpdateAsync(int id, UpdateQuestDto dto);
        Task DeleteAsync(int id);
    }
}
