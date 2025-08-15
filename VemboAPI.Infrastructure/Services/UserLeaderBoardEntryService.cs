using System;
using AutoMapper;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class UserLeaderBoardService : IUserLeaderBoardService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;

        private const string AllKey = "leaderboard_all";
        private static string UserKey(int id) => $"leaderboard_user_{id}";
        private static readonly TimeSpan Ttl = TimeSpan.FromMinutes(1);

        public UserLeaderBoardService(
            VemboDbContext dbContext,
            IMapper mapper,
            ICacheService cache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<List<UserLeaderBoardEntryDto>> GetAllAsync()
        {
            var cached = await _cache.GetAsync<List<UserLeaderBoardEntryDto>>(AllKey);
            if (cached is not null) return cached;

            var entries = await _dbContext.UserLeaderBoardEntries.ToListAsync();
            var result = _mapper.Map<List<UserLeaderBoardEntryDto>>(entries);

            await _cache.SetAsync(AllKey, result, Ttl);
            return result;
        }

        public async Task<UserLeaderBoardEntryDto> GetByIdAsync(int id)
        {
            var key = UserKey(id);
            var cached = await _cache.GetAsync<UserLeaderBoardEntryDto>(key);
            if (cached is not null) return cached;

            var entry = await _dbContext.UserLeaderBoardEntries.FindAsync(id)
                ?? throw new KeyNotFoundException($"Leaderboard entry with ID {id} not found.");

            var dto = _mapper.Map<UserLeaderBoardEntryDto>(entry);
            await _cache.SetAsync(key, dto, Ttl);
            return dto;
        }

        public async Task<UserLeaderBoardEntryDto> CreateAsync(CreateUserLeaderBoardEntryDto dto)
        {
            if (!await _dbContext.Users.AnyAsync(u => u.Id == dto.UserId))
                throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");

            var entity = _mapper.Map<UserLeaderBoardEntry>(dto);
            _dbContext.UserLeaderBoardEntries.Add(entity);
            await _dbContext.SaveChangesAsync();

            // інвалідація кешу
            await _cache.RemoveAsync(AllKey);
            // новий запис ще не має власного Id у ключі user_{id} (ми кешуємо за entry.Id, не userId),
            // але якщо ти кешуєш за entry.Id — зніми цей коментар і видали відповідний ключ.

            return _mapper.Map<UserLeaderBoardEntryDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateUserLeaderBoardEntryDto dto)
        {
            var entry = await _dbContext.UserLeaderBoardEntries.FindAsync(id)
                ?? throw new KeyNotFoundException($"Leaderboard entry with ID {id} not found.");

            _mapper.Map(dto, entry);
            await _dbContext.SaveChangesAsync();

            // інвалідація кешу
            await _cache.RemoveAsync(AllKey);
            await _cache.RemoveAsync(UserKey(id));
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await _dbContext.UserLeaderBoardEntries.FindAsync(id)
                ?? throw new KeyNotFoundException($"Leaderboard entry with ID {id} not found.");

            _dbContext.UserLeaderBoardEntries.Remove(entry);
            await _dbContext.SaveChangesAsync();

            // інвалідація кешу
            await _cache.RemoveAsync(AllKey);
            await _cache.RemoveAsync(UserKey(id));
        }
    }
}
