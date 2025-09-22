using System.Collections.Generic;
using System.Threading.Tasks;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserQuestProgressService
    {
        Task<List<UserQuestProgressDto>> GetAllAsync();
        Task<UserQuestProgressDto> GetByIdsAsync(string userId, int questId);
        Task<UserQuestProgressDto> CreateAsync(CreateUserQuestProgressDto dto);
        Task UpdateAsync(string userId, int questId, UpdateUserQuestProgressDto dto);
        Task DeleteAsync(string userId, int questId);
    }
}
