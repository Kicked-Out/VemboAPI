using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class QuestService : IQuestService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(10);

        public QuestService(VemboDbContext dbContext, IMapper mapper, ICacheService cache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<List<QuestDto>> GetAllAsync()
        {
            return await _cache.GetOrSetAsync("quests:all", async () =>
            {
                var quests = await _dbContext.Quests
                    .AsNoTracking()
                    .Include(q => q.QuestDefinition)
                    .Include(q => q.QuestType)
                    .ToListAsync();
                return _mapper.Map<List<QuestDto>>(quests);
            }, CacheTtl);
        }

        public async Task<QuestDto> GetByIdAsync(int id)
        {
            var cacheKey = $"quests:{id}";
            var cached = await _cache.GetAsync<QuestDto>(cacheKey);
            if (cached is not null)
            {
                return cached;
            }

            var quest = await _dbContext.Quests
                .AsNoTracking()
                .Include(q => q.QuestDefinition)
                .Include(q => q.QuestType)
                .FirstOrDefaultAsync(d => d.Id == id)
                ?? throw new KeyNotFoundException($"Quest with ID {id} not found.");

            var dto = _mapper.Map<QuestDto>(quest);
            await _cache.SetAsync(cacheKey, dto, CacheTtl);
            return dto;
        }

        public async Task<QuestDto> CreateAsync(CreateQuestDto dto)
        {
            await ValidateReferencesAsync(dto.QuestDefinitionId, dto.QuestTypeId);

            var quest = _mapper.Map<Quest>(dto);
            _dbContext.Quests.Add(quest);
            await _dbContext.SaveChangesAsync();

            await InvalidateCacheAsync(quest.Id);
            return _mapper.Map<QuestDto>(quest);
        }

        public async Task UpdateAsync(int id, UpdateQuestDto dto)
        {
            var quest = await _dbContext.Quests.FindAsync(id)
                ?? throw new KeyNotFoundException($"Quest with ID {id} not found.");

            await ValidateReferencesAsync(dto.QuestDefinitionId, dto.QuestTypeId);

            _mapper.Map(dto, quest);
            await _dbContext.SaveChangesAsync();
            await InvalidateCacheAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var quest = await _dbContext.Quests.FindAsync(id)
                ?? throw new KeyNotFoundException($"Quest with ID {id} not found.");

            _dbContext.Quests.Remove(quest);
            await _dbContext.SaveChangesAsync();
            await InvalidateCacheAsync(id);
        }

        private async Task ValidateReferencesAsync(int questDefinitionId, int questTypeId)
        {
            if (!await _dbContext.QuestDefinitions.AnyAsync(q => q.Id == questDefinitionId))
            {
                throw new KeyNotFoundException($"Quest definition with ID {questDefinitionId} not found.");
            }

            if (!await _dbContext.QuestTypes.AnyAsync(qt => qt.Id == questTypeId))
            {
                throw new KeyNotFoundException($"Quest type with ID {questTypeId} not found.");
            }
        }

        private async Task InvalidateCacheAsync(int id)
        {
            await _cache.RemoveAsync("quests:all");
            await _cache.RemoveAsync($"quests:{id}");
        }
    }
}
