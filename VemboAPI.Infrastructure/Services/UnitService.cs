using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class UnitService : IUnitService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public UnitService(
            VemboDbContext dbContext,
            IMapper mapper,
            ICacheService cache,
            IContentVersionService ver)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
            _ver = ver;
        }

        public async Task<List<UnitDto>> GetAllUnits()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:units:all:v{v}";

            var result = await _cache.GetOrSetAsync(key, async () =>
            {
                var units = await _dbContext.Units.ToListAsync();
                
                return _mapper.Map<List<UnitDto>>(units);
            }, ttl: null);

            return result;
        }

        public async Task<UnitDto> GetUnitById(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:unit:{id}:v{v}";

            var dto = await _cache.GetOrSetAsync(key, async () =>
            {
                var unit = await _dbContext.Units.FindAsync(id);
                
                if (unit == null)
                    throw new KeyNotFoundException($"Unit with ID {id} not found.");

                return _mapper.Map<UnitDto>(unit);
            }, ttl: null);

            return dto!;
        }

        public async Task<UnitDto> CreateUnit(CreateUnitDto dto)
        {
            if (!await _dbContext.Topics.AnyAsync(t => t.Id == dto.TopicId))
                throw new KeyNotFoundException($"Topic with ID {dto.TopicId} not found.");

            if (!await _dbContext.GuideBooks.AnyAsync(g => g.Id == dto.GuideBookId))
                throw new KeyNotFoundException($"GuideBook with ID {dto.GuideBookId} not found.");

            var unit = _mapper.Map<Unit>(dto);
            unit.GuideBookId = dto.GuideBookId; // на випадок, якщо AutoMapper не встановить

            await _dbContext.Units.AddAsync(unit);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу через нову версію

            return _mapper.Map<UnitDto>(unit);
        }

        public async Task UpdateUnit(int id, UpdateUnitDto dto)
        {
            var unit = await _dbContext.Units.FindAsync(id);

            if (unit == null)
                throw new KeyNotFoundException($"Unit with ID {id} not found.");

            if (!await _dbContext.Topics.AnyAsync(t => t.Id == dto.TopicId))
                throw new KeyNotFoundException($"Topic with ID {dto.TopicId} not found.");

            if (!await _dbContext.GuideBooks.AnyAsync(g => g.Id == dto.GuideBookId))
                throw new KeyNotFoundException($"GuideBook with ID {dto.GuideBookId} not found.");

            _mapper.Map(dto, unit);
            unit.GuideBookId = dto.GuideBookId;

            await _dbContext.SaveChangesAsync();
            
            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task DeleteUnit(int id)
        {
            var unit = await _dbContext.Units.FindAsync(id);
            
            if (unit == null)
                throw new KeyNotFoundException($"Unit with ID {id} not found.");

            _dbContext.Units.Remove(unit);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task<List<UnitDto>> GetAllUnitsByTopicId(int topicId)
        {
            var units = await _dbContext.Units
                .Where(unit => unit.TopicId == topicId)
                .ToListAsync();

            return _mapper.Map<List<UnitDto>>(units);
        }
    }
}
