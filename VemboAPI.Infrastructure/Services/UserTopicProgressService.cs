using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class UserTopicProgressService : IUserTopicProgressService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserTopicProgressService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserTopicProgressDto>> GetAllUserTopicProgress(string userId)
        {
            var progressList = await _dbContext.UserTopicProgresses
                .Where(topicProgress => topicProgress.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<UserTopicProgressDto>>(progressList);
        }

        public async Task<UserTopicProgressDto> GetUserTopicProgressById(int id)
        {
            var progress = await _dbContext.UserTopicProgresses.FindAsync(id);

            if (progress == null)
                throw new KeyNotFoundException($"UserTopicProgress with ID {id} not found.");

            return _mapper.Map<UserTopicProgressDto>(progress);
        }

        public async Task<UserTopicProgressDto> EnsureProgressExists(string userId, int topicId)
        {
            var existing = await _dbContext.UserTopicProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.TopicId == topicId);

            if (existing != null)
                return _mapper.Map<UserTopicProgressDto>(existing);

            var progress = new UserTopicProgress
            {
                UserId = userId,
                TopicId = topicId,
                CompletedCount = 0
            };

            await _dbContext.UserTopicProgresses.AddAsync(progress);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserTopicProgressDto>(progress);
        }

        public async Task<UserTopicProgressDto> CreateUserTopicProgress(CreateUserTopicProgressDto dto)
        {
            var progress = _mapper.Map<UserTopicProgress>(dto);
            
            await _dbContext.UserTopicProgresses.AddAsync(progress);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserTopicProgressDto>(progress);
        }

        public async Task UpdateUserTopicProgress(int id, UpdateUserTopicProgressDto dto)
        {
            var progress = await _dbContext.UserTopicProgresses.FindAsync(id);
            
            if (progress == null)
                throw new KeyNotFoundException($"UserTopicProgress with ID {id} not found.");

            _mapper.Map(dto, progress);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserTopicProgress(int id)
        {
            var progress = await _dbContext.UserTopicProgresses.FindAsync(id);

            if (progress == null)
                throw new KeyNotFoundException($"UserTopicProgress with ID {id} not found.");

            _dbContext.UserTopicProgresses.Remove(progress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserTopicProgressDto[]> GetAllUserTopicProgressByPeriodId(string userId, int periodId)
        {
            var progresses = await _dbContext.UserTopicProgresses
                .Include(topicProgress => topicProgress.Topic)
                .Where(topicProgress => topicProgress.Topic.PeriodId == periodId)
                .ToListAsync();

            return _mapper.Map<UserTopicProgressDto[]>(progresses);
        }

        public async Task<UserTopicProgressDto> GetCurrentUserTopicProgress(string userId, int periodId)
        {
            var progress = await _dbContext.UserTopicProgresses
                .Where(topicProgress => topicProgress.UserId == userId && topicProgress.Topic.PeriodId == periodId)
                .LastOrDefaultAsync();

            return _mapper.Map<UserTopicProgressDto>(progress);
        }
    }
}