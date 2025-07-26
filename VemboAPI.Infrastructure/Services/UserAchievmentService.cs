using System;
using AutoMapper;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class UserAchievementService : IUserAchievementService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserAchievementService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserAchievementDto>> GetAllAsync()
        {
            var list = await _dbContext.UserAchievements.ToListAsync();
            return _mapper.Map<List<UserAchievementDto>>(list);
        }

        public async Task<UserAchievementDto> GetByIdAsync(int id)
        {
            var entry = await _dbContext.UserAchievements.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserAchievement with ID {id} not found.");

            return _mapper.Map<UserAchievementDto>(entry);
        }

        public async Task<UserAchievementDto> CreateAsync(CreateUserAchievementDto dto)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.Id == dto.UserId))
                throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");
            if (!await _dbContext.Achievements.AnyAsync(x => x.Id == dto.AchievementId))
                throw new KeyNotFoundException($"Achievement with ID {dto.AchievementId} not found.");

            var entity = _mapper.Map<UserAchievement>(dto);
            _dbContext.UserAchievements.Add(entity);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UserAchievementDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateUserAchievementDto dto)
        {
            var entity = await _dbContext.UserAchievements.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserAchievement with ID {id} not found.");

            _mapper.Map(dto, entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.UserAchievements.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserAchievement with ID {id} not found.");

            _dbContext.UserAchievements.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

}

