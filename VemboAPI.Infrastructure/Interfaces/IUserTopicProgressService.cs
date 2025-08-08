using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserTopicProgressService
    {
        List<UserTopicProgressDto> GetAllUserTopicProgress();
        UserTopicProgressDto GetUserTopicProgressById(int id);
        UserTopicProgressDto EnsureProgressExists(int userId, int topicId);
        UserTopicProgressDto CreateUserTopicProgress(CreateUserTopicProgressDto dto);
        void UpdateUserTopicProgress(int id, UpdateUserTopicProgressDto dto);
        void DeleteUserTopicProgress(int id);
    }
}
