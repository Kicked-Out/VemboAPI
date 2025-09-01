using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class UserStreakService : IUserStreakService
    {
        private readonly VemboDbContext _context;
        private readonly IMapper _mapper;

        public UserStreakService(VemboDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserStreakDto>> GetAllAsync()
        {
            var entities = await _context.UserStreaks.ToListAsync();
            return _mapper.Map<List<UserStreakDto>>(entities);
        }

        public async Task<UserStreakDto> GetByIdAsync(int id)
        {
            var entity = await _context.UserStreaks.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserStreak with ID {id} not found.");
            return _mapper.Map<UserStreakDto>(entity);
        }

        public async Task<UserStreakDto> CreateAsync(CreateUserStreakDto dto)
        {
            if (!await _context.Users.AnyAsync(u => u.Id == dto.UserId))
                throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");

            var entity = _mapper.Map<UserStreak>(dto);
            _context.UserStreaks.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserStreakDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateUserStreakDto dto)
        {
            var entity = await _context.UserStreaks.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserStreak with ID {id} not found.");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.UserStreaks.FindAsync(id)
                ?? throw new KeyNotFoundException($"UserStreak with ID {id} not found.");
            _context.UserStreaks.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
