using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class UserLevelProgressService : IUserLevelProgressService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserLevelProgressService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<UserLevelProgressDto> GetAllUserLevelProgress()
        {
            var progresses = _dbContext.UserLevelProgresses.ToList();
            return _mapper.Map<List<UserLevelProgressDto>>(progresses);
        }

        public UserLevelProgressDto GetUserLevelProgressById(int id)
        {
            var progress = _dbContext.UserLevelProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserLevelProgress with ID {id} not found.");

            return _mapper.Map<UserLevelProgressDto>(progress);
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

            return _mapper.Map<UserLevelProgressDto>(progress);
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
