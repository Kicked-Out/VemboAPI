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
    public class QuestTypeService : IQuestTypeService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(30);

        public QuestTypeService(VemboDbContext dbContext, IMapper mapper, ICacheService cache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<List<QuestTypeDto>> GetAllAsync()
        {
            return await _cache.GetOrSetAsync("quest-types:all", async () =>
            {
                var types = await _dbContext.QuestTypes
                    .AsNoTracking()
                    .ToListAsync();
                return _mapper.Map<List<QuestTypeDto>>(types);
            }, CacheTtl);
        }

        public async Task<QuestTypeDto> GetByIdAsync(int id)
        {
            var cacheKey = $"quest-types:{id}";
            var cached = await _cache.GetAsync<QuestTypeDto>(cacheKey);
            if (cached is not null)
            {
                return cached;
            }

            var entity = await _dbContext.QuestTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException($"Quest type with ID {id} not found.");

            var dto = _mapper.Map<QuestTypeDto>(entity);
            await _cache.SetAsync(cacheKey, dto, CacheTtl);
            return dto;
        }

        public async Task<QuestTypeDto> CreateAsync(CreateQuestTypeDto dto)
        {
            var entity = _mapper.Map<QuestType>(dto);
            _dbContext.QuestTypes.Add(entity);
            await _dbContext.SaveChangesAsync();

            await InvalidateCacheAsync(entity.Id);
            return _mapper.Map<QuestTypeDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateQuestTypeDto dto)
        {
            var entity = await _dbContext.QuestTypes.FindAsync(id)
                ?? throw new KeyNotFoundException($"Quest type with ID {id} not found.");

            _mapper.Map(dto, entity);
            await _dbContext.SaveChangesAsync();
            await InvalidateCacheAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.QuestTypes.FindAsync(id)
                ?? throw new KeyNotFoundException($"Quest type with ID {id} not found.");

            _dbContext.QuestTypes.Remove(entity);
            await _dbContext.SaveChangesAsync();
            await InvalidateCacheAsync(id);
        }

        private async Task InvalidateCacheAsync(int id)
        {
            await _cache.RemoveAsync("quest-types:all");
            await _cache.RemoveAsync($"quest-types:{id}");
        }
    }
}
