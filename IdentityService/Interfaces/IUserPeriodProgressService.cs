using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserPeriodProgressService
    {
        List<UserPeriodProgressDto> GetAllUserPeriodProgress(string userId);
        UserPeriodProgressDto GetUserPeriodProgressById(int id);
        UserPeriodProgressDto GetUserPeriodProgressByPeriodId(string userId, int periodId);
        UserPeriodProgressDto GetUserPeriodProgressWithMostXPByUserId(string userId);
        UserPeriodProgressDto CreateUserPeriodProgress(CreateUserPeriodProgressDto dto);
        void UpdateUserPeriodProgress(int id, UpdateUserPeriodProgressDto dto);
        void DeleteUserPeriodProgress(int id);
        UserPeriodProgressDto EnsureProgressExists(int userId, int periodId);
    }
}
