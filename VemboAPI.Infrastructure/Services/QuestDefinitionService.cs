using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class QuestDefinitionService : IQuestDefinitionService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(10);

        public QuestDefinitionService(VemboDbContext dbContext, IMapper mapper, ICacheService cache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<List<QuestDefinitionDto>> GetAllAsync()
        {
            return await _cache.GetOrSetAsync("quest-definitions:all", async () =>
            {
                var quests = await _dbContext.QuestDefinitions
                    .AsNoTracking()
                    .ToListAsync();
                return _mapper.Map<List<QuestDefinitionDto>>(quests);
            }, CacheTtl);
        }

        public async Task<QuestDefinitionDto> GetByIdAsync(int id)
        {
            var cacheKey = $"quest-definitions:{id}";
            var cached = await _cache.GetAsync<QuestDefinitionDto>(cacheKey);
            if (cached is not null)
            {
                return cached;
            }

            var quest = await _dbContext.QuestDefinitions
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id)
                ?? throw new KeyNotFoundException($"Quest definition with ID {id} not found.");

            var dto = _mapper.Map<QuestDefinitionDto>(quest);
            await _cache.SetAsync(cacheKey, dto, CacheTtl);
            return dto;
        }

        public async Task<QuestDefinitionDto> CreateAsync(CreateQuestDefinitionDto dto)
        {
            var quest = _mapper.Map<QuestDefinition>(dto);
            _dbContext.QuestDefinitions.Add(quest);
            await _dbContext.SaveChangesAsync();

            await InvalidateCacheAsync(quest.Id);
            return _mapper.Map<QuestDefinitionDto>(quest);
        }

        public async Task UpdateAsync(int id, UpdateQuestDefinitionDto dto)
        {
            var quest = await _dbContext.QuestDefinitions.FindAsync(id)
                ?? throw new KeyNotFoundException($"Quest definition with ID {id} not found.");

            _mapper.Map(dto, quest);
            await _dbContext.SaveChangesAsync();
            await InvalidateCacheAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var quest = await _dbContext.QuestDefinitions.FindAsync(id)
                ?? throw new KeyNotFoundException($"Quest definition with ID {id} not found.");

            _dbContext.QuestDefinitions.Remove(quest);
            await _dbContext.SaveChangesAsync();
            await InvalidateCacheAsync(id);
        }

        private async Task InvalidateCacheAsync(int id)
        {
            await _cache.RemoveAsync("quest-definitions:all");
            await _cache.RemoveAsync($"quest-definitions:{id}");
        }
    }
}
