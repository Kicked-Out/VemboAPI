using System;
using AutoMapper;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class AchievementLevelService : IAchievementLevelService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public AchievementLevelService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<AchievementLevelDto>> GetAllAsync()
        {
            var levels = await _dbContext.AchievementLevels.ToListAsync();
            return _mapper.Map<List<AchievementLevelDto>>(levels);
        }

        public async Task<AchievementLevelDto> GetByIdAsync(int id)
        {
            var level = await _dbContext.AchievementLevels.FindAsync(id)
                ?? throw new KeyNotFoundException($"AchievementLevel with ID {id} not found.");

            return _mapper.Map<AchievementLevelDto>(level);
        }

        public async Task<AchievementLevelDto> CreateAsync(CreateAchievementLevelDto dto)
        {
            if (!await _dbContext.Achievements.AnyAsync(a => a.Id == dto.AchievementId))
                throw new KeyNotFoundException($"Achievement with ID {dto.AchievementId} not found.");

            var level = _mapper.Map<AchievementLevel>(dto);
            _dbContext.AchievementLevels.Add(level);
            await _dbContext.SaveChangesAsync();

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
        }

        public async Task DeleteAsync(int id)
        {
            var level = await _dbContext.AchievementLevels.FindAsync(id)
                ?? throw new KeyNotFoundException($"AchievementLevel with ID {id} not found.");

            _dbContext.AchievementLevels.Remove(level);
            await _dbContext.SaveChangesAsync();
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

