using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserLevelProgressService
    {
        List<UserLevelProgressDto> GetAllUserLevelProgress(string userId);
        UserLevelProgressDto GetUserLevelProgressById(int id);
        UserLevelProgressDto GetUserLevelProgressByLevelId(string userId, int levelId);
        UserLevelProgressDto CreateUserLevelProgress(CreateUserLevelProgressDto dto);
        void UpdateUserLevelProgress(int id, UpdateUserLevelProgressDto dto);
        UserLevelProgressDto EnsureProgressExists(string userId, int levelId);

        void DeleteUserLevelProgress(int id);
    }
}
