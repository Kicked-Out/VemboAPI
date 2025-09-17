using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserLevelProgressService
    {
        Task<List<UserLevelProgressDto>> GetAllUserLevelProgress(string userId);
        Task<UserLevelProgressDto> GetUserLevelProgressById(int id);
        Task<UserLevelProgressDto> GetUserLevelProgressByLevelId(string userId, int levelId);
        Task<UserLevelProgressDto> CreateUserLevelProgress(CreateUserLevelProgressDto dto);
        Task UpdateUserLevelProgress(int id, UpdateUserLevelProgressDto dto);
        Task<UserLevelProgressDto> EnsureProgressExists(string userId, int levelId);

        Task DeleteUserLevelProgress(int id);
    }
}
