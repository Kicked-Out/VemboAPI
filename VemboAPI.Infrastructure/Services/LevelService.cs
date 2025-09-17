using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class LevelService : ILevelService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public LevelService(
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

        public async Task<List<LevelDto>> GetAllLevels()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:levels:all:v{v}";

            var result = await _cache.GetOrSetAsync(key, async () =>
            {
                var levels = await _dbContext.Levels.ToListAsync(); // синхронно ок
                var mapped = _mapper.Map<List<LevelDto>>(levels);
                
                return mapped;
            }, ttl: null);

            return result;
        }

        public async Task<LevelDto> GetLevelById(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:level:{id}:v{v}";

            var dto = await _cache.GetOrSetAsync(key, async () =>
            {
                var level = await  _dbContext.Levels.FindAsync(id);
                
                if (level == null)
                    throw new KeyNotFoundException($"Level with ID {id} not found.");

                var mapped = _mapper.Map<LevelDto>(level);
                
                return mapped;
            }, ttl: null);

            return dto!;
        }

        public async Task<LevelDto> CreateLevel(CreateLevelDto dto)
        {
            if (!await _dbContext.Units.AnyAsync(u => u.Id == dto.UnitId))
                throw new KeyNotFoundException($"Unit with ID {dto.UnitId} not found.");

            if (!await _dbContext.LevelTypes.AnyAsync(lt => lt.Id == dto.LevelTypeId))
                throw new KeyNotFoundException($"LevelType with ID {dto.LevelTypeId} not found.");

            var level = _mapper.Map<Level>(dto);
            level.LevelTypeId = dto.LevelTypeId;

            await _dbContext.Levels.AddAsync(level);
            await _dbContext.SaveChangesAsync();
            await _ver.BumpAsync(); // інвалідація кешу

            return _mapper.Map<LevelDto>(level);
        }

        public async Task UpdateLevel(int id, UpdateLevelDto dto)
        {
            var level = await _dbContext.Levels.FindAsync(id);
            
            if (level == null)
                throw new KeyNotFoundException($"Level with ID {id} not found.");

            if (!_dbContext.Units.Any(u => u.Id == dto.UnitId))
                throw new KeyNotFoundException($"Unit with ID {dto.UnitId} not found.");

            if (!_dbContext.LevelTypes.Any(lt => lt.Id == dto.LevelTypeId))
                throw new KeyNotFoundException($"LevelType with ID {dto.LevelTypeId} not found.");

            _mapper.Map(dto, level);
            level.LevelTypeId = dto.LevelTypeId;

            await _dbContext.SaveChangesAsync();
            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task DeleteLevel(int id)
        {
            var level = await _dbContext.Levels.FindAsync(id);
            
            if (level == null)
                throw new KeyNotFoundException($"Level with ID {id} not found.");

            _dbContext.Levels.Remove(level);
            await _dbContext.SaveChangesAsync();
            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task<List<LevelDto>> GetAllLevelsByUnitId(int unitId)
        {
            var levels = await _dbContext.Levels
                .Where(level => level.UnitId == unitId)
                .ToListAsync();

            return _mapper.Map<List<LevelDto>>(levels);
        }
    }
}
