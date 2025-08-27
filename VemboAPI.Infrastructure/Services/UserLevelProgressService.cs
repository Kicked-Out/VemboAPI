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

        public List<UserLevelProgressDto> GetAllUserLevelProgress(string userId)
        {
            var progresses = _dbContext.UserLevelProgresses.ToList().FindAll(levelProgress => levelProgress.UserId == userId);
            return _mapper.Map<List<UserLevelProgressDto>>(progresses);
        }

        public UserLevelProgressDto GetUserLevelProgressById(int id)
        {
            var progress = _dbContext.UserLevelProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserLevelProgress with ID {id} not found.");

            return _mapper.Map<UserLevelProgressDto>(progress);
        }
        public UserLevelProgressDto EnsureProgressExists(int userId, int levelId)
        {
            var existing = _dbContext.UserLevelProgresses
                .FirstOrDefault(p => p.UserId == userId && p.LevelId == levelId);

            if (existing != null)
                return _mapper.Map<UserLevelProgressDto>(existing);

            var progress = new UserLevelProgress
            {
                UserId = userId,
                LevelId = levelId,
                isCompleted = false
            };

            _dbContext.UserLevelProgresses.Add(progress);
            _dbContext.SaveChanges();

            return _mapper.Map<UserLevelProgressDto>(progress);
        }

        public UserLevelProgressDto GetUserLevelProgressByLevelId(string userId, int levelId)
        {
            var progress = _dbContext.UserLevelProgresses
                .ToList()
                .FindAll(levelProgress => levelProgress.UserId == userId)
                .Find(levelProgress => levelProgress.LevelId == levelId);

            if (progress == null)
            {
                throw new KeyNotFoundException($"UserLevelProgress with LevelId {levelId} not found.");
            }

            return _mapper.Map<UserLevelProgressDto>(progress);
        }

        public UserLevelProgressDto CreateUserLevelProgress(CreateUserLevelProgressDto dto)
        {
            var progress = _mapper.Map<UserLevelProgress>(dto);

            _dbContext.UserLevelProgresses.Add(progress);
            _dbContext.SaveChanges();

            return _mapper.Map<UserLevelProgressDto>(progress);
        }


        public void UpdateUserLevelProgress(int id, UpdateUserLevelProgressDto dto)
        {
            var progress = _dbContext.UserLevelProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserLevelProgress with ID {id} not found.");

            _mapper.Map(dto, progress);
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
