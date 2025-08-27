using System;
using AutoMapper;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class AchievementLevelService : IAchievementLevelService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public AchievementLevelService(
            VemboDbContext dbContext,
            IMapper mapper,
            ICacheService cache,
            IContentVersionService ver)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
            _ver = ver;
        }

        public async Task<List<AchievementLevelDto>> GetAllAsync()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:achievement-levels:all:v{v}";

            return await _cache.GetOrSetAsync(key, async () =>
            {
                var levels = await _dbContext.AchievementLevels.ToListAsync();
                return _mapper.Map<List<AchievementLevelDto>>(levels);
            }, ttl: null);
        }

        public async Task<AchievementLevelDto> GetByIdAsync(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:achievement-level:{id}:v{v}";

            return await _cache.GetOrSetAsync(key, async () =>
            {
                var level = await _dbContext.AchievementLevels.FindAsync(id)
                    ?? throw new KeyNotFoundException($"AchievementLevel with ID {id} not found.");
                return _mapper.Map<AchievementLevelDto>(level);
            }, ttl: null);
        }

        public async Task<AchievementLevelDto> CreateAsync(CreateAchievementLevelDto dto)
        {
            if (!await _dbContext.Achievements.AnyAsync(a => a.Id == dto.AchievementId))
                throw new KeyNotFoundException($"Achievement with ID {dto.AchievementId} not found.");

            var level = _mapper.Map<AchievementLevel>(dto);
            _dbContext.AchievementLevels.Add(level);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу (через нову версію)

            return _mapper.Map<AchievementLevelDto>(level);
        }

        public async Task UpdateAsync(int id, UpdateAchievementLevelDto dto)
        {
            var level = await _dbContext.AchievementLevels.FindAsync(id)
                ?? throw new KeyNotFoundException($"AchievementLevel with ID {id} not found.");

            if (!await _dbContext.Achievements.AnyAsync(a => a.Id == dto.AchievementId))
                throw new KeyNotFoundException($"Achievement with ID {dto.AchievementId} not found.");

            _mapper.Map(dto, level);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task DeleteAsync(int id)
        {
            var level = await _dbContext.AchievementLevels.FindAsync(id)
                ?? throw new KeyNotFoundException($"AchievementLevel with ID {id} not found.");

            _dbContext.AchievementLevels.Remove(level);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task<AchievementLevelDto> GetByAchievementIdAndLevelId(int achievementId, int levelId)
        {
            var level = _dbContext.AchievementLevels
                .ToList()
                .FindAll(achievementLevel => achievementLevel.AchievementId == achievementId)
                .Find(achievementLevel => achievementLevel.Level == levelId);

            return _mapper.Map<AchievementLevelDto>(level);
        }
    }
}
