using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Services
{
    public class UserLevelProgressService : IUserLevelProgressService
    {
        private readonly VemboDbContext _dbContext;

        public UserLevelProgressService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserLevelProgressDto> GetAllUserLevelProgress()
        {
            return _dbContext.UserLevelProgresses
                .Select(ulp => new UserLevelProgressDto
                {
                    Id = ulp.Id,
                    UserId = ulp.UserId,
                    LevelId = ulp.LevelId,
                    isCompleted = ulp.isCompleted
                })
                .ToList();
        }

        public UserLevelProgressDto GetUserLevelProgressById(int id)
        {
            var progress = _dbContext.UserLevelProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserLevelProgress with ID {id} not found.");

            return new UserLevelProgressDto
            {
                Id = progress.Id,
                UserId = progress.UserId,
                LevelId = progress.LevelId,
                isCompleted = progress.isCompleted
            };
        }

        public UserLevelProgressDto CreateUserLevelProgress(int userId, int levelId, bool isCompleted)
        {
            var progress = new UserLevelProgress
            {
                UserId = userId,
                LevelId = levelId,
                isCompleted = isCompleted
            };

            _dbContext.UserLevelProgresses.Add(progress);
            _dbContext.SaveChanges();

            return new UserLevelProgressDto
            {
                Id = progress.Id,
                UserId = progress.UserId,
                LevelId = progress.LevelId,
                isCompleted = progress.isCompleted
            };
        }

        public void UpdateUserLevelProgress(int id, int userId, int levelId, bool isCompleted)
        {
            var progress = _dbContext.UserLevelProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserLevelProgress with ID {id} not found.");

            progress.UserId = userId;
            progress.LevelId = levelId;
            progress.isCompleted = isCompleted;

            _dbContext.UserLevelProgresses.Update(progress);
            _dbContext.SaveChanges();
        }

        public void DeleteUserLevelProgress(int id)
        {
            var progress = _dbContext.UserLevelProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserLevelProgress with ID {id} not found.");

            _dbContext.UserLevelProgresses.Remove(progress);
            _dbContext.SaveChanges();
        }
    }
}
