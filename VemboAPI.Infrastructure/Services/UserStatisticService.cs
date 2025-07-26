using System;
using AutoMapper;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class UserStatisticService : IUserStatisticService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserStatisticService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserStatisticDto>> GetAllAsync()
        {
            var stats = await _dbContext.UserStatistics.ToListAsync();
            return _mapper.Map<List<UserStatisticDto>>(stats);
        }

        public async Task<UserStatisticDto> GetByIdAsync(int id)
        {
            var stat = await _dbContext.UserStatistics.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserStatistic with ID {id} not found.");

            return _mapper.Map<UserStatisticDto>(stat);
        }

        public async Task<UserStatisticDto> CreateAsync(CreateUserStatisticDto dto)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.Id == dto.UserId))
                throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");

            if (!await _dbContext.Periods.AnyAsync(x => x.Id == dto.CurrentPeriodId))
                throw new KeyNotFoundException($"Period with ID {dto.CurrentPeriodId} not found.");

            var entity = _mapper.Map<UserStatistic>(dto);
            _dbContext.UserStatistics.Add(entity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserStatisticDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateUserStatisticDto dto)
        {
            var stat = await _dbContext.UserStatistics.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserStatistic with ID {id} not found.");

            _mapper.Map(dto, stat);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var stat = await _dbContext.UserStatistics.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserStatistic with ID {id} not found.");

            _dbContext.UserStatistics.Remove(stat);
            await _dbContext.SaveChangesAsync();
        }
    }

}

