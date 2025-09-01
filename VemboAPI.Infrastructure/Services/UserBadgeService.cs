using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class UserBadgeService : IUserBadgeService
    {
        private readonly VemboDbContext _context;
        private readonly IMapper _mapper;

        public UserBadgeService(VemboDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserBadgeDto>> GetAllAsync()
        {
            var items = await _context.UserBadges.ToListAsync();
            return _mapper.Map<List<UserBadgeDto>>(items);
        }

        public async Task<UserBadgeDto> GetByIdAsync(int id)
        {
            var entity = await _context.UserBadges.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Not found");
            return _mapper.Map<UserBadgeDto>(entity);
        }

        public async Task<UserBadgeDto> CreateAsync(CreateUserBadgeDto dto)
        {
            var entity = _mapper.Map<UserBadge>(dto);
            _context.UserBadges.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserBadgeDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateUserBadgeDto dto)
        {
            var entity = await _context.UserBadges.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Not found");
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.UserBadges.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Not found");
            _context.UserBadges.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
