using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserTopicProgressService
    {
        List<UserTopicProgressDto> GetAllUserTopicProgress(string userId);
        UserTopicProgressDto GetUserTopicProgressById(int id);
        UserTopicProgressDto[] GetAllUserTopicProgressByPeriodId(string userId, int periodId);
        UserTopicProgressDto GetCurrentUserTopicProgress(string userId, int periodId);
        UserTopicProgressDto EnsureProgressExists(string userId, int topicId);
        UserTopicProgressDto CreateUserTopicProgress(CreateUserTopicProgressDto dto);
        void UpdateUserTopicProgress(int id, UpdateUserTopicProgressDto dto);
        void DeleteUserTopicProgress(int id);
    }
}
