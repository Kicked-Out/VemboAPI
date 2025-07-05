using System;
using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserTopicProgressService
    {
        List<UserTopicProgressDto> GetAllUserTopicProgress();
        UserTopicProgressDto GetUserTopicProgressById(int id);
        UserTopicProgressDto CreateUserTopicProgress(int userId, int topicId, bool isCompleted);
        void UpdateUserTopicProgress(int id, int userId, int topicId, bool isCompleted);
        void DeleteUserTopicProgress(int id);
    }
}
