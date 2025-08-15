using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

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

        public List<UnitDto> GetAllUnits()
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:units:all:v{v}";

            var result = _cache.GetOrSetAsync(key, async () =>
            {
                var units = _dbContext.Units.ToList();
                return _mapper.Map<List<UnitDto>>(units);
            }, ttl: null).GetAwaiter().GetResult();

            return result;
        }

        public UnitDto GetUnitById(int id)
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:unit:{id}:v{v}";

            var dto = _cache.GetOrSetAsync(key, async () =>
            {
                var unit = _dbContext.Units.Find(id);
                if (unit == null)
                    throw new KeyNotFoundException($"Unit with ID {id} not found.");

                return _mapper.Map<UnitDto>(unit);
            }, ttl: null).GetAwaiter().GetResult();

            return dto!;
        }

        public UnitDto CreateUnit(CreateUnitDto dto)
        {
            if (!_dbContext.Topics.Any(t => t.Id == dto.TopicId))
                throw new KeyNotFoundException($"Topic with ID {dto.TopicId} not found.");

            if (!_dbContext.GuideBooks.Any(g => g.Id == dto.GuideBookId))
                throw new KeyNotFoundException($"GuideBook with ID {dto.GuideBookId} not found.");

            var unit = _mapper.Map<Unit>(dto);
            unit.GuideBookId = dto.GuideBookId; // на випадок, якщо AutoMapper не встановить

            _dbContext.Units.Add(unit);
            _dbContext.SaveChanges();
            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу через нову версію

            return _mapper.Map<UnitDto>(unit);
        }

        public void UpdateUnit(int id, UpdateUnitDto dto)
        {
            var unit = _dbContext.Units.Find(id);
            if (unit == null)
                throw new KeyNotFoundException($"Unit with ID {id} not found.");

            if (!_dbContext.Topics.Any(t => t.Id == dto.TopicId))
                throw new KeyNotFoundException($"Topic with ID {dto.TopicId} not found.");

            if (!_dbContext.GuideBooks.Any(g => g.Id == dto.GuideBookId))
                throw new KeyNotFoundException($"GuideBook with ID {dto.GuideBookId} not found.");

            _mapper.Map(dto, unit);
            unit.GuideBookId = dto.GuideBookId;

            _dbContext.SaveChanges();
            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }

        public void DeleteUnit(int id)
        {
            var unit = _dbContext.Units.Find(id);
            if (unit == null)
                throw new KeyNotFoundException($"Unit with ID {id} not found.");

            _dbContext.Units.Remove(unit);
            _dbContext.SaveChanges();
            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }
    }
}
