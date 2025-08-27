using System.Collections.Generic;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserUnitProgressService
    {
        List<UserUnitProgressDto> GetAllUserUnitProgress(string userId);
        UserUnitProgressDto GetUserUnitProgressById(int id);
        List<UserUnitProgressDto> GetAllUserUnitProgressByTopicId(string userId, int topicId);
        UserUnitProgressDto GetUserUnitProgressByUnitId(string userId, int unitId);
        UserUnitProgressDto GetCurrentUserUnitProgress(string userId, int topicId);
        UserUnitProgressDto CreateUserUnitProgress(CreateUserUnitProgressDto dto);
        void UpdateUserUnitProgress(int id, UpdateUserUnitProgressDto dto);

        void DeleteUserUnitProgress(int id);
    }
}
