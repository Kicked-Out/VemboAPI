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
    public class UserQuestProgressService : IUserQuestProgressService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(5);

        public UserQuestProgressService(VemboDbContext dbContext, IMapper mapper, ICacheService cache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<List<UserQuestProgressDto>> GetAllAsync()
        {
            return await _cache.GetOrSetAsync("user-quest-progress:all", async () =>
            {
                var entries = await _dbContext.UserQuestProgresses
                    .AsNoTracking()
                    .Include(x => x.Quest)
                    .ToListAsync();
                return _mapper.Map<List<UserQuestProgressDto>>(entries);
            }, CacheTtl);
        }

        public async Task<UserQuestProgressDto> GetByIdsAsync(string userId, int questId)
        {
            var cacheKey = GetCacheKey(userId, questId);
            var cached = await _cache.GetAsync<UserQuestProgressDto>(cacheKey);
            if (cached is not null)
            {
                return cached;
            }

            var entity = await _dbContext.UserQuestProgresses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.QuestId == questId)
                ?? throw new KeyNotFoundException($"Quest progress for user {userId} and quest {questId} not found.");

            var dto = _mapper.Map<UserQuestProgressDto>(entity);
            await _cache.SetAsync(cacheKey, dto, CacheTtl);
            return dto;
        }

        public async Task<UserQuestProgressDto> CreateAsync(CreateUserQuestProgressDto dto)
        {
            await ValidateReferencesAsync(dto.UserId, dto.QuestId);

            var entity = _mapper.Map<UserQuestProgress>(dto);
            _dbContext.UserQuestProgresses.Add(entity);
            await _dbContext.SaveChangesAsync();

            await InvalidateCacheAsync(dto.UserId, dto.QuestId);
            return _mapper.Map<UserQuestProgressDto>(entity);
        }

        public async Task UpdateAsync(string userId, int questId, UpdateUserQuestProgressDto dto)
        {
            var entity = await _dbContext.UserQuestProgresses.FindAsync(userId, questId)
                ?? throw new KeyNotFoundException($"Quest progress for user {userId} and quest {questId} not found.");

            _mapper.Map(dto, entity);
            await _dbContext.SaveChangesAsync();
            await InvalidateCacheAsync(userId, questId);
        }

        public async Task DeleteAsync(string userId, int questId)
        {
            var entity = await _dbContext.UserQuestProgresses.FindAsync(userId, questId)
                ?? throw new KeyNotFoundException($"Quest progress for user {userId} and quest {questId} not found.");

            _dbContext.UserQuestProgresses.Remove(entity);
            await _dbContext.SaveChangesAsync();
            await InvalidateCacheAsync(userId, questId);
        }

        private async Task ValidateReferencesAsync(string userId, int questId)
        {
            if (!await _dbContext.Users.AnyAsync(u => u.Id == userId))
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            if (!await _dbContext.Quests.AnyAsync(q => q.Id == questId))
            {
                throw new KeyNotFoundException($"Quest with ID {questId} not found.");
            }
        }

        private async Task InvalidateCacheAsync(string userId, int questId)
        {
            await _cache.RemoveAsync("user-quest-progress:all");
            await _cache.RemoveAsync(GetCacheKey(userId, questId));
        }

        private static string GetCacheKey(string userId, int questId) => $"user-quest-progress:{userId}:{questId}";
    }
}
