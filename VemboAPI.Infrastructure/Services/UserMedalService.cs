using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class UserMedalService : IUserMedalService
    {
        private readonly VemboDbContext _context;
        private readonly IMapper _mapper;

        public UserMedalService(VemboDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserMedalDto>> GetAllAsync()
        {
            var items = await _context.UserMedals.ToListAsync();
            return _mapper.Map<List<UserMedalDto>>(items);
        }

        public async Task<UserMedalDto> GetByIdAsync(int id)
        {
            var entity = await _context.UserMedals.FindAsync(id) ?? throw new KeyNotFoundException($"UserMedal with ID {id} not found.");
            return _mapper.Map<UserMedalDto>(entity);
        }

        public async Task<UserMedalDto> CreateAsync(CreateUserMedalDto dto)
        {
            if (!await _context.Users.AnyAsync(x => x.Id == dto.UserId))
                throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");
            if (!await _context.Medals.AnyAsync(x => x.Id == dto.MedalId))
                throw new KeyNotFoundException($"Medal with ID {dto.MedalId} not found.");

            var entity = _mapper.Map<UserMedal>(dto);
            _context.UserMedals.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserMedalDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateUserMedalDto dto)
        {
            var entity = await _context.UserMedals.FindAsync(id) ?? throw new KeyNotFoundException($"UserMedal with ID {id} not found.");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.UserMedals.FindAsync(id) ?? throw new KeyNotFoundException($"UserMedal with ID {id} not found.");
            _context.UserMedals.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
