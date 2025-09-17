using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class UserStreakDayService : IUserStreakDayService
    {
        private readonly VemboDbContext _context;
        private readonly IMapper _mapper;

        public UserStreakDayService(VemboDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserStreakDayDto>> GetAllAsync()
        {
            var entities = await _context.UserStreakDays.ToListAsync();
            return _mapper.Map<List<UserStreakDayDto>>(entities);
        }

        public async Task<UserStreakDayDto> GetByIdAsync(int id)
        {
            var entity = await _context.UserStreakDays.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserStreakDay with ID {id} not found.");
            return _mapper.Map<UserStreakDayDto>(entity);
        }

        public async Task<UserStreakDayDto> CreateAsync(CreateUserStreakDayDto dto)
        {
            if (!await _context.Users.AnyAsync(u => u.Id == dto.UserId))
                throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");

            var entity = _mapper.Map<UserStreakDay>(dto);
            _context.UserStreakDays.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserStreakDayDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateUserStreakDayDto dto)
        {
            var entity = await _context.UserStreakDays.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserStreakDay with ID {id} not found.");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.UserStreakDays.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserStreakDay with ID {id} not found.");
            _context.UserStreakDays.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
