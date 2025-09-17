using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<UserLevelProgressDto>> GetAllUserLevelProgress(string userId)
        {
            var progresses = await _dbContext.UserLevelProgresses.Where(levelProgress => levelProgress.UserId == userId).ToListAsync();

            return _mapper.Map<List<UserLevelProgressDto>>(progresses);
        }

        public async Task<UserLevelProgressDto> GetUserLevelProgressById(int id)
        {
            var progress = await _dbContext.UserLevelProgresses.FindAsync(id);
            
            if (progress == null)
                throw new KeyNotFoundException($"UserLevelProgress with ID {id} not found.");

            return _mapper.Map<UserLevelProgressDto>(progress);
        }
        public async Task<UserLevelProgressDto> EnsureProgressExists(string userId, int levelId)
        {
            var existing = await _dbContext.UserLevelProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.LevelId == levelId);

            if (existing != null)
                return _mapper.Map<UserLevelProgressDto>(existing);

            var progress = new UserLevelProgress
            {
                UserId = userId,
                LevelId = levelId,
                CompletedCount = 0
            };

            await _dbContext.UserLevelProgresses.AddAsync(progress);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserLevelProgressDto>(progress);
        }

        public async Task<UserLevelProgressDto> GetUserLevelProgressByLevelId(string userId, int levelId)
        {
            var progress = await _dbContext.UserLevelProgresses
                .Where(levelProgress => levelProgress.UserId == userId)
                .Where(levelProgress => levelProgress.LevelId == levelId)
                .FirstOrDefaultAsync();

            if (progress == null)
            {
                throw new KeyNotFoundException($"UserLevelProgress with LevelId {levelId} not found.");
            }

            return _mapper.Map<UserLevelProgressDto>(progress);
        }

        public async Task<UserLevelProgressDto> CreateUserLevelProgress(CreateUserLevelProgressDto dto)
        {
            var progress = _mapper.Map<UserLevelProgress>(dto);

            await _dbContext.UserLevelProgresses.AddAsync(progress);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserLevelProgressDto>(progress);
        }


        public async Task UpdateUserLevelProgress(int id, UpdateUserLevelProgressDto dto)
        {
            var progress = await _dbContext.UserLevelProgresses.FindAsync(id);

            if (progress == null)
                throw new KeyNotFoundException($"UserLevelProgress with ID {id} not found.");

            _mapper.Map(dto, progress);
            
            await _dbContext.SaveChangesAsync();
        }



        public async Task DeleteUserLevelProgress(int id)
        {
            var progress = await _dbContext.UserLevelProgresses.FindAsync(id);

            if (progress == null)
                throw new KeyNotFoundException($"UserLevelProgress with ID {id} not found.");

            _dbContext.UserLevelProgresses.Remove(progress);
            await _dbContext.SaveChangesAsync();
        }
    }
}
