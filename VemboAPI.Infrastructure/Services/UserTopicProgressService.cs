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

        public List<UserTopicProgressDto> GetAllUserTopicProgress(string userId)
        {
            var progressList = _dbContext.UserTopicProgresses
                .ToList()
                .FindAll(topicProgress => topicProgress.UserId == userId);
            return _mapper.Map<List<UserTopicProgressDto>>(progressList);
        }

        public UserTopicProgressDto GetUserTopicProgressById(int id)
        {
            var progress = _dbContext.UserTopicProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserTopicProgress with ID {id} not found.");

            return _mapper.Map<UserTopicProgressDto>(progress);
        }

        public UserTopicProgressDto EnsureProgressExists(string userId, int topicId)
        {
            var existing = _dbContext.UserTopicProgresses
                .FirstOrDefault(p => p.UserId == userId && p.TopicId == topicId);

            if (existing != null)
                return _mapper.Map<UserTopicProgressDto>(existing);

            var progress = new UserTopicProgress
            {
                UserId = userId,
                TopicId = topicId,
                CompletedCount = 0
            };

            _dbContext.UserTopicProgresses.Add(progress);
            _dbContext.SaveChanges();

            return _mapper.Map<UserTopicProgressDto>(progress);
        }

        public UserTopicProgressDto CreateUserTopicProgress(CreateUserTopicProgressDto dto)
        {
            var progress = _mapper.Map<UserTopicProgress>(dto);
            _dbContext.UserTopicProgresses.Add(progress);
            _dbContext.SaveChanges();

            return _mapper.Map<UserTopicProgressDto>(progress);
        }

        public void UpdateUserTopicProgress(int id, UpdateUserTopicProgressDto dto)
        {
            var progress = _dbContext.UserTopicProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserTopicProgress with ID {id} not found.");

            _mapper.Map(dto, progress);
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

        public UserTopicProgressDto[] GetAllUserTopicProgressByPeriodId(string userId, int periodId)
        {
            var progresses = _dbContext.UserTopicProgresses
                .Include(topicProgress => topicProgress.Topic)
                .Where(topicProgress => topicProgress.Topic.PeriodId == periodId)
                .ToList();

            return _mapper.Map<UserTopicProgressDto[]>(progresses);
        }

        public UserTopicProgressDto GetCurrentUserTopicProgress(string userId, int periodId)
        {
            var progress = _dbContext.UserTopicProgresses
                .ToList()
                .FindAll(topicProgress => topicProgress.UserId == userId && topicProgress.Topic.PeriodId == periodId)
                .LastOrDefault();

            return _mapper.Map<UserTopicProgressDto>(progress);
        }
    }
}