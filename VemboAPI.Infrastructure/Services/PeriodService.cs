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

        public async Task<List<PeriodDto>> GetAllPeriods()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:periods:v{v}";

            var result = await _cache.GetOrSetAsync(key, async () =>
            {
                var periods = await _dbContext.Periods
                    .Include(p => p.Topics)
                    .ToListAsync();

                var mapped = _mapper.Map<List<PeriodDto>>(periods);
                
                for (int i = 0; i < mapped.Count; i++)
                    mapped[i].TopicsCount = periods[i].Topics.Count;

                return mapped;
            }, ttl: null);

            return result;
        }

        public async Task<PeriodDto> GetPeriodById(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:period:{id}:v{v}";

            var dto = await _cache.GetOrSetAsync(key, async () =>
            {
                var period = await _dbContext.Periods
                    .Include(p => p.Topics)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (period == null)
                    throw new KeyNotFoundException($"Period with ID {id} not found.");

                var mapped = _mapper.Map<PeriodDto>(period);
                mapped.TopicsCount = period.Topics.Count;
                return mapped;
            }, ttl: null);

            return dto!;
        }

        public async Task<PeriodDto> CreatePeriod(CreatePeriodDto dto)
        {
            var period = _mapper.Map<Period>(dto);
            
            await _dbContext.Periods.AddAsync(period);
            await _dbContext.SaveChangesAsync();

            // bump content version, щоб інвалідувати ключі (через v{ver})
            await _ver.BumpAsync();

            return _mapper.Map<PeriodDto>(period);
        }

        public async Task UpdatePeriod(int id, UpdatePeriodDto dto)
        {
            var period = await _dbContext.Periods.FindAsync(id);

            if (period == null)
                throw new KeyNotFoundException($"Period with ID {id} not found.");

            _mapper.Map(dto, period);
            
            await _dbContext.SaveChangesAsync();

            // інвалідуємо через bump версії
            await _ver.BumpAsync();
        }

        public async Task DeletePeriod(int id)
        {
            var period = await _dbContext.Periods.FindAsync(id);

            if (period == null)
                throw new KeyNotFoundException($"Period with ID {id} not found.");

            _dbContext.Periods.Remove(period);
            await _dbContext.SaveChangesAsync();

            // інвалідуємо через bump версії
            await _ver.BumpAsync();
        }
    }
}
