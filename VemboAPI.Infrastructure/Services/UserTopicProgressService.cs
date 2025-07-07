using VemboAPI.Domain.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Services
{
    public class UserTopicProgressService : IUserTopicProgressService
    {
        private readonly VemboDbContext _dbContext;

        public UserTopicProgressService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserTopicProgressDto> GetAllUserTopicProgress()
        {
            return _dbContext.UserTopicProgresses
                .Select(utp => new UserTopicProgressDto
                {
                    Id = utp.Id,
                    UserId = utp.UserId,
                    TopicId = utp.TopicId,
                    isCompleted = utp.isCompleted
                })
                .ToList();
        }

        public UserTopicProgressDto GetUserTopicProgressById(int id)
        {
            var progress = _dbContext.UserTopicProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserTopicProgress with ID {id} not found.");

            return new UserTopicProgressDto
            {
                Id = progress.Id,
                UserId = progress.UserId,
                TopicId = progress.TopicId,
                isCompleted = progress.isCompleted
            };
        }

        public UserTopicProgressDto CreateUserTopicProgress(int userId, int topicId, bool isCompleted)
        {
            var progress = new UserTopicProgress
            {
                UserId = userId,
                TopicId = topicId,
                isCompleted = isCompleted
            };

            _dbContext.UserTopicProgresses.Add(progress);
            _dbContext.SaveChanges();

            return new UserTopicProgressDto
            {
                Id = progress.Id,
                UserId = progress.UserId,
                TopicId = progress.TopicId,
                isCompleted = progress.isCompleted
            };
        }

        public void UpdateUserTopicProgress(int id, int userId, int topicId, bool isCompleted)
        {
            var progress = _dbContext.UserTopicProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserTopicProgress with ID {id} not found.");

            progress.UserId = userId;
            progress.TopicId = topicId;
            progress.isCompleted = isCompleted;

            _dbContext.UserTopicProgresses.Update(progress);
            _dbContext.SaveChanges();
        }

        public void DeleteUserTopicProgress(int id)
        {
            var progress = _dbContext.UserTopicProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserTopicProgress with ID {id} not found.");

            _dbContext.UserTopicProgresses.Remove(progress);
            _dbContext.SaveChanges();
        }
    }
}
