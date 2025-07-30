using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserLevelProgressService
    {
        List<UserLevelProgressDto> GetAllUserLevelProgress();
        UserLevelProgressDto GetUserLevelProgressById(int id);
        UserLevelProgressDto CreateUserLevelProgress(CreateUserLevelProgressDto dto);
        void UpdateUserLevelProgress(int id, UpdateUserLevelProgressDto dto);
        UserLevelProgressDto EnsureProgressExists(int userId, int levelId);

        void DeleteUserLevelProgress(int id);
    }
}
