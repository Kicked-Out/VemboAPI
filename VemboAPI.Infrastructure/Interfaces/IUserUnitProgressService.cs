using System.Collections.Generic;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserUnitProgressService
    {
        List<UserUnitProgressDto> GetAllUserUnitProgress();
        UserUnitProgressDto GetUserUnitProgressById(int id);
        UserUnitProgressDto CreateUserUnitProgress(int userId, int unitId, bool isCompleted);
        void UpdateUserUnitProgress(int id, int userId, int unitId, bool isCompleted);
        void DeleteUserUnitProgress(int id);
    }
}
