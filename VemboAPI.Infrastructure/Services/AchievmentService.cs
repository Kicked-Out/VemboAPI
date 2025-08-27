using AutoMapper;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class AchievementService : IAchievementService
    {
        private readonly VemboDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public AchievementService(
            VemboDbContext context,
            IMapper mapper,
            ICacheService cache,
            IContentVersionService ver)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
            _ver = ver;
        }

        public async Task<List<AchievementDto>> GetAllAsync()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:achievements:all:v{v}";

            return await _cache.GetOrSetAsync(key, async () =>
            {
                var items = await _context.Achievements.ToListAsync();
                return _mapper.Map<List<AchievementDto>>(items);
            }, ttl: null);
        }

        public async Task<AchievementDto> GetByIdAsync(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:achievement:{id}:v{v}";

            return await _cache.GetOrSetAsync(key, async () =>
            {
                var item = await _context.Achievements.FindAsync(id);
                if (item == null) throw new Exception("Not found");
                return _mapper.Map<AchievementDto>(item);
            }, ttl: null);
        }

        public async Task<AchievementDto> CreateAsync(CreateAchievementDto dto)
        {
            var entity = _mapper.Map<Achievement>(dto);
            _context.Achievements.Add(entity);
            await _context.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація через нову версію

            return _mapper.Map<AchievementDto>(entity);
        }

        public async Task<AchievementDto> UpdateAsync(int id, UpdateAchievementDto dto)
        {
            var entity = await _context.Achievements.FindAsync(id);
            if (entity == null) throw new Exception("Not found");

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу

            return _mapper.Map<AchievementDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Achievements.FindAsync(id);
            if (entity == null) throw new Exception("Not found");

            _context.Achievements.Remove(entity);
            await _context.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }
    }
}
