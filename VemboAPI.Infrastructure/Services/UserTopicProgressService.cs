using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

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

        public List<UserTopicProgressDto> GetAllUserTopicProgress()
        {
            var progressList = _dbContext.UserTopicProgresses.ToList();
            return _mapper.Map<List<UserTopicProgressDto>>(progressList);
        }

        public UserTopicProgressDto GetUserTopicProgressById(int id)
        {
            var progress = _dbContext.UserTopicProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserTopicProgress with ID {id} not found.");

            return _mapper.Map<UserTopicProgressDto>(progress);
        }

        public UserTopicProgressDto CreateUserTopicProgress(CreateUserTopicProgressDto dto)
        {
            var progress = new UserTopicProgress
            {
                UserId = dto.UserId,
                TopicId = dto.TopicId,
                isCompleted = dto.isCompleted
            };

            _dbContext.UserTopicProgresses.Add(progress);
            _dbContext.SaveChanges();

            return _mapper.Map<UserTopicProgressDto>(progress);
        }

        public void UpdateUserTopicProgress(int id, UpdateUserTopicProgressDto dto)
        {
            var progress = _dbContext.UserTopicProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"UserTopicProgress with ID {id} not found.");

            progress.UserId = dto.UserId;
            progress.TopicId = dto.TopicId;
            progress.isCompleted = dto.isCompleted;

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
