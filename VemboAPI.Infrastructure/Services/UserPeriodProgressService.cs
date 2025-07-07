using VemboAPI.Domain.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Services
{
    public class UserPeriodProgressService : IUserPeriodProgressService
    {
        private readonly VemboDbContext _dbContext;

        public UserPeriodProgressService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserPeriodProgressDto> GetAllUserPeriodProgress()
        {
            return _dbContext.UserPeriodProgresses
                .Select(upp => new UserPeriodProgressDto
                {
                    Id = upp.Id,
                    UserId = upp.UserId,
                    PeriodId = upp.PeriodId,
                    isCompleted = upp.isCompleted
                })
                .ToList();
        }

        public UserPeriodProgressDto GetUserPeriodProgressById(int id)
        {
            var progress = _dbContext.UserPeriodProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserPeriodProgress with ID {id} not found.");

            return new UserPeriodProgressDto
            {
                Id = progress.Id,
                UserId = progress.UserId,
                PeriodId = progress.PeriodId,
                isCompleted = progress.isCompleted
            };
        }

        public UserPeriodProgressDto CreateUserPeriodProgress(int userId, int periodId, bool isCompleted)
        {
            var progress = new UserPeriodProgress
            {
                UserId = userId,
                PeriodId = periodId,
                isCompleted = isCompleted
            };

            _dbContext.UserPeriodProgresses.Add(progress);
            _dbContext.SaveChanges();

            return new UserPeriodProgressDto
            {
                Id = progress.Id,
                UserId = progress.UserId,
                PeriodId = progress.PeriodId,
                isCompleted = progress.isCompleted
            };
        }

        public void UpdateUserPeriodProgress(int id, int userId, int periodId, bool isCompleted)
        {
            var progress = _dbContext.UserPeriodProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserPeriodProgress with ID {id} not found.");

            progress.UserId = userId;
            progress.PeriodId = periodId;
            progress.isCompleted = isCompleted;

            _dbContext.UserPeriodProgresses.Update(progress);
            _dbContext.SaveChanges();
        }

        public void DeleteUserPeriodProgress(int id)
        {
            var progress = _dbContext.UserPeriodProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserPeriodProgress with ID {id} not found.");

            _dbContext.UserPeriodProgresses.Remove(progress);
            _dbContext.SaveChanges();
        }
    }
}
