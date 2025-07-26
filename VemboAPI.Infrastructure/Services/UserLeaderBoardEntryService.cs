using System;
using AutoMapper;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class UserLeaderBoardService : IUserLeaderBoardService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserLeaderBoardService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserLeaderBoardEntryDto>> GetAllAsync()
        {
            var entries = await _dbContext.UserLeaderBoardEntries.ToListAsync();
            return _mapper.Map<List<UserLeaderBoardEntryDto>>(entries);
        }

        public async Task<UserLeaderBoardEntryDto> GetByIdAsync(int id)
        {
            var entry = await _dbContext.UserLeaderBoardEntries.FindAsync(id)
                ?? throw new KeyNotFoundException($"Leaderboard entry with ID {id} not found.");

            return _mapper.Map<UserLeaderBoardEntryDto>(entry);
        }

        public async Task<UserLeaderBoardEntryDto> CreateAsync(CreateUserLeaderBoardEntryDto dto)
        {
            if (!await _dbContext.Users.AnyAsync(u => u.Id == dto.UserId))
                throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");

            var entity = _mapper.Map<UserLeaderBoardEntry>(dto);
            _dbContext.UserLeaderBoardEntries.Add(entity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserLeaderBoardEntryDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateUserLeaderBoardEntryDto dto)
        {
            var entry = await _dbContext.UserLeaderBoardEntries.FindAsync(id)
                ?? throw new KeyNotFoundException($"Leaderboard entry with ID {id} not found.");

            _mapper.Map(dto, entry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await _dbContext.UserLeaderBoardEntries.FindAsync(id)
                ?? throw new KeyNotFoundException($"Leaderboard entry with ID {id} not found.");

            _dbContext.UserLeaderBoardEntries.Remove(entry);
            await _dbContext.SaveChangesAsync();
        }
    }

}

