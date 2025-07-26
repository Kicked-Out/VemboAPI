using System;
using AutoMapper;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class AchievementService : IAchievementService
    {
        private readonly VemboDbContext _context;
        private readonly IMapper _mapper;

        public AchievementService(VemboDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AchievementDto>> GetAllAsync()
        {
            var items = await _context.Achievements.ToListAsync();
            return _mapper.Map<List<AchievementDto>>(items);
        }

        public async Task<AchievementDto> GetByIdAsync(int id)
        {
            var item = await _context.Achievements.FindAsync(id);
            if (item == null) throw new Exception("Not found");
            return _mapper.Map<AchievementDto>(item);
        }

        public async Task<AchievementDto> CreateAsync(CreateAchievementDto dto)
        {
            var entity = _mapper.Map<Achievement>(dto);
            _context.Achievements.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AchievementDto>(entity);
        }

        public async Task<AchievementDto> UpdateAsync(int id, UpdateAchievementDto dto)
        {
            var entity = await _context.Achievements.FindAsync(id);
            if (entity == null) throw new Exception("Not found");

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AchievementDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Achievements.FindAsync(id);
            if (entity == null) throw new Exception("Not found");

            _context.Achievements.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

}

