using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class PeriodService : IPeriodService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public PeriodService(
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

        public List<PeriodDto> GetAllPeriods()
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:periods:v{v}";

            var result = _cache.GetOrSetAsync(key, async () =>
            {
                var periods = await _dbContext.Periods
                    .Include(p => p.Topics)
                    .ToListAsync();

                var mapped = _mapper.Map<List<PeriodDto>>(periods);
                for (int i = 0; i < mapped.Count; i++)
                    mapped[i].TopicsCount = periods[i].Topics.Count;

                return mapped;
            }, ttl: null).GetAwaiter().GetResult();

            return result;
        }

        public PeriodDto GetPeriodById(int id)
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:period:{id}:v{v}";

            var dto = _cache.GetOrSetAsync(key, async () =>
            {
                var period = await _dbContext.Periods
                    .Include(p => p.Topics)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (period == null)
                    throw new KeyNotFoundException($"Period with ID {id} not found.");

                var mapped = _mapper.Map<PeriodDto>(period);
                mapped.TopicsCount = period.Topics.Count;
                return mapped;
            }, ttl: null).GetAwaiter().GetResult();

            return dto!;
        }

        public PeriodDto CreatePeriod(CreatePeriodDto dto)
        {
            var period = _mapper.Map<Period>(dto);
            _dbContext.Periods.Add(period);
            _dbContext.SaveChanges();

            // bump content version, щоб інвалідувати ключі (через v{ver})
            _ver.BumpAsync().GetAwaiter().GetResult();

            return _mapper.Map<PeriodDto>(period);
        }

        public void UpdatePeriod(int id, UpdatePeriodDto dto)
        {
            var period = _dbContext.Periods.Find(id);
            if (period == null)
                throw new KeyNotFoundException($"Period with ID {id} not found.");

            _mapper.Map(dto, period);
            _dbContext.SaveChanges();

            // інвалідуємо через bump версії
            _ver.BumpAsync().GetAwaiter().GetResult();
        }

        public void DeletePeriod(int id)
        {
            var period = _dbContext.Periods.Find(id);
            if (period == null)
                throw new KeyNotFoundException($"Period with ID {id} not found.");

            _dbContext.Periods.Remove(period);
            _dbContext.SaveChanges();

            // інвалідуємо через bump версії
            _ver.BumpAsync().GetAwaiter().GetResult();
        }
    }
}
