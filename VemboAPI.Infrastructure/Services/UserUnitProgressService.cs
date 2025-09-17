using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class UserUnitProgressService : IUserUnitProgressService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserUnitProgressService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserUnitProgressDto>> GetAllUserUnitProgress(string userId)
        {
            var unitProgressList = await _dbContext.UserUnitProgresses
                .Where(unitProgress => unitProgress.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<UserUnitProgressDto>>(unitProgressList);
        }

        public async Task<UserUnitProgressDto> GetUserUnitProgressById(int id)
        {
            var progress = await _dbContext.UserUnitProgresses.FindAsync(id);

            if (progress == null)
                throw new KeyNotFoundException($"UserUnitProgress with ID {id} not found.");

            return _mapper.Map<UserUnitProgressDto>(progress);
        }
        public async Task<UserUnitProgressDto> EnsureProgressExists(string userId, int unitId)
        {
            var existing = await _dbContext.UserUnitProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.UnitId == unitId);

            if (existing != null)
                return _mapper.Map<UserUnitProgressDto>(existing);

            var progress = new UserUnitProgress
            {
                UserId = userId,
                UnitId = unitId,
                CompletedCount = 0
            };

            await _dbContext.UserUnitProgresses.AddAsync(progress);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserUnitProgressDto>(progress);
        }


        public async Task<UserUnitProgressDto> CreateUserUnitProgress(CreateUserUnitProgressDto dto)
        {
            var progress = _mapper.Map<UserUnitProgress>(dto);

            await _dbContext.UserUnitProgresses.AddAsync(progress);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserUnitProgressDto>(progress);
        }

        public async Task UpdateUserUnitProgress(int id, UpdateUserUnitProgressDto dto)
        {
            var progress = await _dbContext.UserUnitProgresses.FindAsync(id);

            if (progress == null)
                throw new KeyNotFoundException($"UserUnitProgress with ID {id} not found.");

            _mapper.Map(dto, progress);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserUnitProgress(int id)
        {
            var progress = await _dbContext.UserUnitProgresses.FindAsync(id);

            if (progress == null)
                throw new KeyNotFoundException($"UserUnitProgress with ID {id} not found.");

            _dbContext.UserUnitProgresses.Remove(progress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserUnitProgressDto>> GetAllUserUnitProgressByTopicId(string userId, int topicId)
        {
            var progresses = await _dbContext.UserUnitProgresses
                .Where(unitProgress => unitProgress.UserId == userId)
                .Where(unitProgress => unitProgress.Unit.TopicId == topicId)
                .ToListAsync();

            return _mapper.Map<List<UserUnitProgressDto>>(progresses);
        }

        public async Task<UserUnitProgressDto> GetUserUnitProgressByUnitId(string userId, int unitId)
        {
            var progress = await _dbContext.UserUnitProgresses
                .Where(unitProgress => unitProgress.UserId == userId)
                .Where(unitProgress => unitProgress.UnitId == unitId)
                .FirstOrDefaultAsync();

            return _mapper.Map<UserUnitProgressDto>(progress);
        }

        public async Task<UserUnitProgressDto> GetCurrentUserUnitProgress(string userId, int topicId)
        {
            var progress = await _dbContext.UserUnitProgresses
                .Where(unitProgress => unitProgress.UserId == userId)
                .Where(unitProgress => unitProgress.Unit.TopicId == topicId)
                .LastOrDefaultAsync();

            return _mapper.Map<UserUnitProgressDto>(progress);
        }
    }

}
