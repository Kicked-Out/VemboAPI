using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Services
{
    public class UserUnitProgressService : IUserUnitProgressService
    {
        private readonly VemboDbContext _dbContext;

        public UserUnitProgressService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserUnitProgressDto> GetAllUserUnitProgress()
        {
            return _dbContext.UserUnitProgresses
                .Select(uup => new UserUnitProgressDto
                {
                    Id = uup.Id,
                    UserId = uup.UserId,
                    UnitId = uup.UnitId,
                    isCompleted = uup.isCompleted
                })
                .ToList();
        }

        public UserUnitProgressDto GetUserUnitProgressById(int id)
        {
            var progress = _dbContext.UserUnitProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserUnitProgress with ID {id} not found.");

            return new UserUnitProgressDto
            {
                Id = progress.Id,
                UserId = progress.UserId,
                UnitId = progress.UnitId,
                isCompleted = progress.isCompleted
            };
        }

        public UserUnitProgressDto CreateUserUnitProgress(int userId, int unitId, bool isCompleted)
        {
            var progress = new UserUnitProgress
            {
                UserId = userId,
                UnitId = unitId,
                isCompleted = isCompleted
            };

            _dbContext.UserUnitProgresses.Add(progress);
            _dbContext.SaveChanges();

            return new UserUnitProgressDto
            {
                Id = progress.Id,
                UserId = progress.UserId,
                UnitId = progress.UnitId,
                isCompleted = progress.isCompleted
            };
        }

        public void UpdateUserUnitProgress(int id, int userId, int unitId, bool isCompleted)
        {
            var progress = _dbContext.UserUnitProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserUnitProgress with ID {id} not found.");

            progress.UserId = userId;
            progress.UnitId = unitId;
            progress.isCompleted = isCompleted;

            _dbContext.UserUnitProgresses.Update(progress);
            _dbContext.SaveChanges();
        }

        public void DeleteUserUnitProgress(int id)
        {
            var progress = _dbContext.UserUnitProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserUnitProgress with ID {id} not found.");

            _dbContext.UserUnitProgresses.Remove(progress);
            _dbContext.SaveChanges();
        }
    }
}
