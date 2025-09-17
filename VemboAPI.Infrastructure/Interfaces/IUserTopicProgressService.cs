using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserTopicProgressService
    {
        Task<List<UserTopicProgressDto>> GetAllUserTopicProgress(string userId);
        Task<UserTopicProgressDto> GetUserTopicProgressById(int id);
        Task<UserTopicProgressDto[]> GetAllUserTopicProgressByPeriodId(string userId, int periodId);
        Task<UserTopicProgressDto> GetCurrentUserTopicProgress(string userId, int periodId);
        Task<UserTopicProgressDto> EnsureProgressExists(string userId, int topicId);
        Task<UserTopicProgressDto> CreateUserTopicProgress(CreateUserTopicProgressDto dto);
        Task UpdateUserTopicProgress(int id, UpdateUserTopicProgressDto dto);
        Task DeleteUserTopicProgress(int id);
    }
}
