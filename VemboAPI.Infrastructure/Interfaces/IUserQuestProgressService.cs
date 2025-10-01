using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserQuestProgressService
    {
        Task<List<UserQuestProgressDto>> GetAllAsync();
        Task<UserQuestProgressDto> GetByIdAsync(int id);
        Task<UserQuestProgressDto> GetByQuestId(string userId, int questId);
        Task<List<UserQuestProgressDto>> GetAllMonthly();
        Task<UserQuestProgressDto> CreateAsync(CreateUserQuestProgressDto dto);
        Task UpdateAsync(int id, UpdateUserQuestProgressDto dto);
        Task DeleteAsync(int id);
    }
}
