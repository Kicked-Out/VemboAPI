using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserPeriodProgressService
    {
        Task<List<UserPeriodProgressDto>> GetAllUserPeriodProgress(string userId);
        Task<UserPeriodProgressDto> GetUserPeriodProgressById(int id);
        Task<UserPeriodProgressDto> GetUserPeriodProgressByPeriodId(string userId, int periodId);
        Task<UserPeriodProgressDto> GetUserPeriodProgressWithMostXPByUserId(string userId);
        Task<UserPeriodProgressDto> CreateUserPeriodProgress(CreateUserPeriodProgressDto dto);
        Task UpdateUserPeriodProgress(int id, UpdateUserPeriodProgressDto dto);
        Task DeleteUserPeriodProgress(int id);
        Task<UserPeriodProgressDto> EnsureProgressExists(string userId, int periodId);
    }
}
