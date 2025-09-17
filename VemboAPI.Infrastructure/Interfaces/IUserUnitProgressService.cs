using System.Collections.Generic;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserUnitProgressService
    {
        Task<List<UserUnitProgressDto>> GetAllUserUnitProgress(string userId);
        Task<UserUnitProgressDto> GetUserUnitProgressById(int id);
        Task<List<UserUnitProgressDto>> GetAllUserUnitProgressByTopicId(string userId, int topicId);
        Task<UserUnitProgressDto> GetUserUnitProgressByUnitId(string userId, int unitId);
        Task<UserUnitProgressDto> GetCurrentUserUnitProgress(string userId, int topicId);
        Task<UserUnitProgressDto> CreateUserUnitProgress(CreateUserUnitProgressDto dto);
        Task UpdateUserUnitProgress(int id, UpdateUserUnitProgressDto dto);
        Task<UserUnitProgressDto> EnsureProgressExists(string userId, int unitId);

        Task DeleteUserUnitProgress(int id);
    }
}
