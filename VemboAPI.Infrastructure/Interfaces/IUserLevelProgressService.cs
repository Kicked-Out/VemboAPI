using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserLevelProgressService
    {
        List<UserLevelProgressDto> GetAllUserLevelProgress();
        UserLevelProgressDto GetUserLevelProgressById(int id);
        UserLevelProgressDto CreateUserLevelProgress(int userId, int levelId, bool isCompleted);
        void UpdateUserLevelProgress(int id, int userId, int levelId, bool isCompleted);
        void DeleteUserLevelProgress(int id);
    }
}
