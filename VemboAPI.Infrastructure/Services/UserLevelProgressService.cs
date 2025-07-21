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

        public UserLevelProgressDto CreateUserLevelProgress(CreateUserLevelProgressDto dto)
        {
            var progress = new UserLevelProgress
            {
                UserId = dto.UserId,
                LevelId = dto.LevelId,
                isCompleted = dto.isCompleted
            };

            _dbContext.UserLevelProgresses.Add(progress);
            _dbContext.SaveChanges();

            return _mapper.Map<UserLevelProgressDto>(progress);
        }

        public void UpdateUserLevelProgress(int id, UpdateUserLevelProgressDto dto)
        {
            var progress = _dbContext.UserLevelProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserLevelProgress with ID {id} not found.");

            progress.UserId = dto.UserId;
            progress.LevelId = dto.LevelId;
            progress.isCompleted = dto.isCompleted;

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
