using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

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

        public List<LevelDto> GetAllLevels()
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:levels:all:v{v}";

            var result = _cache.GetOrSetAsync(key, () =>
            {
                var levels = _dbContext.Levels.ToList(); // синхронно ок
                var mapped = _mapper.Map<List<LevelDto>>(levels);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return result;
        }

        public LevelDto GetLevelById(int id)
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:level:{id}:v{v}";

            var dto = _cache.GetOrSetAsync(key, () =>
            {
                var level = _dbContext.Levels.Find(id);
                if (level == null)
                    throw new KeyNotFoundException($"Level with ID {id} not found.");

                var mapped = _mapper.Map<LevelDto>(level);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return dto!;
        }

        public LevelDto CreateLevel(CreateLevelDto dto)
        {
            if (!_dbContext.Units.Any(u => u.Id == dto.UnitId))
                throw new KeyNotFoundException($"Unit with ID {dto.UnitId} not found.");

            if (!_dbContext.LevelTypes.Any(lt => lt.Id == dto.LevelTypeId))
                throw new KeyNotFoundException($"LevelType with ID {dto.LevelTypeId} not found.");

            var level = _mapper.Map<Level>(dto);
            level.LevelTypeId = dto.LevelTypeId;

            _dbContext.Levels.Add(level);
            _dbContext.SaveChanges();
            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу

            return _mapper.Map<LevelDto>(level);
        }

        public void UpdateLevel(int id, UpdateLevelDto dto)
        {
            var level = _dbContext.Levels.Find(id);
            if (level == null)
                throw new KeyNotFoundException($"Level with ID {id} not found.");

            if (!_dbContext.Units.Any(u => u.Id == dto.UnitId))
                throw new KeyNotFoundException($"Unit with ID {dto.UnitId} not found.");

            if (!_dbContext.LevelTypes.Any(lt => lt.Id == dto.LevelTypeId))
                throw new KeyNotFoundException($"LevelType with ID {dto.LevelTypeId} not found.");

            _mapper.Map(dto, level);
            level.LevelTypeId = dto.LevelTypeId;

            _dbContext.SaveChanges();
            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }

        public void DeleteLevel(int id)
        {
            var level = _dbContext.Levels.Find(id);
            if (level == null)
                throw new KeyNotFoundException($"Level with ID {id} not found.");

            _dbContext.Levels.Remove(level);
            _dbContext.SaveChanges();
            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }
    }
}
