using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class UserPeriodProgressService : IUserPeriodProgressService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserPeriodProgressService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserPeriodProgressDto>> GetAllUserPeriodProgress(string userId)
        {
            var progresses = await _dbContext.UserPeriodProgresses
                .Where(periodProgress => periodProgress.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<UserPeriodProgressDto>>(progresses);
        }

        public async Task<UserPeriodProgressDto> GetUserPeriodProgressById(int id)
        {
            var progress = await _dbContext.UserPeriodProgresses.FindAsync(id);
            
            if (progress == null)
                throw new KeyNotFoundException($"UserPeriodProgress with ID {id} not found.");

            return _mapper.Map<UserPeriodProgressDto>(progress);
        }

        public async Task<UserPeriodProgressDto> EnsureProgressExists(string userId, int periodId)
        {
            var existing = await _dbContext.UserPeriodProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.PeriodId == periodId);

            if (existing != null)
                return _mapper.Map<UserPeriodProgressDto>(existing);

            var progress = new UserPeriodProgress
            {
                UserId = userId,
                PeriodId = periodId,
                CompletedCount = 0

            };

            await _dbContext.UserPeriodProgresses.AddAsync(progress);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserPeriodProgressDto>(progress);
        }

        public async Task<UserPeriodProgressDto> CreateUserPeriodProgress(CreateUserPeriodProgressDto dto)
        {
            var progress = _mapper.Map<UserPeriodProgress>(dto);
            
            await _dbContext.UserPeriodProgresses.AddAsync(progress);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserPeriodProgressDto>(progress);
        }

        public async Task UpdateUserPeriodProgress(int id, UpdateUserPeriodProgressDto dto)
        {
            var progress = await _dbContext.UserPeriodProgresses.FindAsync(id);
            
            if (progress == null)
                throw new KeyNotFoundException($"UserPeriodProgress with ID {id} not found.");

            _mapper.Map(dto, progress);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserPeriodProgress(int id)
        {
            var progress = await _dbContext.UserPeriodProgresses.FindAsync(id);
            
            if (progress == null)
                throw new KeyNotFoundException($"UserPeriodProgress with ID {id} not found.");

            _dbContext.UserPeriodProgresses.Remove(progress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserPeriodProgressDto> GetUserPeriodProgressByPeriodId(string userId, int periodId)
        {
            var progress = await _dbContext.UserPeriodProgresses
                .Where(periodProgress => periodProgress.UserId == userId)
                .Where(periodProgress => periodProgress.PeriodId == periodId)
                .FirstOrDefaultAsync();

            if (progress == null)
            {
                throw new KeyNotFoundException($"$UserPeriodProgress with PeriodId {periodId} not found.");
            }

            return _mapper.Map<UserPeriodProgressDto>(progress);
        }

        public async Task<UserPeriodProgressDto> GetUserPeriodProgressWithMostXPByUserId(string userId)
        {
            var progresses = await _dbContext.UserPeriodProgresses
                .Where(userPeriodProgress => userPeriodProgress.UserId == userId)
                .ToListAsync();
            
            var progress = progresses.OrderByDescending(progress => progress.XP).FirstOrDefault();

            return _mapper.Map<UserPeriodProgressDto>(progress);
        }
    }
}
