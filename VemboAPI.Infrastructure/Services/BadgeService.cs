using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly VemboDbContext _context;
        private readonly IMapper _mapper;

        public BadgeService(VemboDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BadgeDto>> GetAllAsync()
        {
            var items = await _context.Badges.ToListAsync();
            return _mapper.Map<List<BadgeDto>>(items);
        }

        public async Task<BadgeDto> GetByIdAsync(int id)
        {
            var entity = await _context.Badges.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Not found");
            return _mapper.Map<BadgeDto>(entity);
        }

        public async Task<BadgeDto> CreateAsync(CreateBadgeDto dto)
        {
            var entity = _mapper.Map<Badge>(dto);
            _context.Badges.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<BadgeDto>(entity);
        }

        public async Task<BadgeDto> UpdateAsync(int id, UpdateBadgeDto dto)
        {
            var entity = await _context.Badges.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<BadgeDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Badges.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Not found");
            _context.Badges.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
