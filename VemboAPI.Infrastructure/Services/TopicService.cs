using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class TopicService : ITopicService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public TopicService(
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

        public async Task<List<TopicDto>> GetAllTopics()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:topics:all:v{v}";

            var result = await _cache.GetOrSetAsync(key, async () =>
            {
                var topics = await _dbContext.Topics
                    .Include(t => t.Units)
                    .ToListAsync();

                var mapped = _mapper.Map<List<TopicDto>>(topics);
                for (int i = 0; i < mapped.Count; i++)
                {
                    mapped[i].UnitsCount = topics[i].Units.Count;
                }
                return mapped;
            }, ttl: null);

            return result;
        }

        public async Task<TopicDto> GetTopicById(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:topic:{id}:v{v}";

            var dto = await _cache.GetOrSetAsync(key, async () =>
            {
                var topic = await _dbContext.Topics
                    .Include(t => t.Units)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (topic == null)
                    throw new KeyNotFoundException($"Topic with ID {id} not found.");

                var mapped = _mapper.Map<TopicDto>(topic);
                mapped.UnitsCount = topic.Units.Count;
                return mapped;
            }, ttl: null);

            return dto!;
        }

        public async Task<TopicDto> CreateTopic(TopicCreateDto dto)
        {
            if (!await _dbContext.Periods.AnyAsync(p => p.Id == dto.PeriodId))
                throw new KeyNotFoundException($"Period with ID {dto.PeriodId} not found.");

            var topic = _mapper.Map<Topic>(dto);

            await _dbContext.Topics.AddAsync(topic);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідує через нову версію

            var result = _mapper.Map<TopicDto>(topic);
            result.UnitsCount = 0;

            return result;
        }

        public async Task UpdateTopic(int id, TopicUpdateDto dto)
        {
            var topic = await _dbContext.Topics.FindAsync(id);

            if (topic == null)
                throw new KeyNotFoundException($"Topic with ID {id} not found.");

            if (!await _dbContext.Periods.AnyAsync(p => p.Id == dto.PeriodId))
                throw new KeyNotFoundException($"Period with ID {dto.PeriodId} not found.");

            _mapper.Map(dto, topic);

            await _dbContext.SaveChangesAsync();
            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task DeleteTopic(int id)
        {
            var topic = await _dbContext.Topics.FindAsync(id);

            if (topic == null)
                throw new KeyNotFoundException($"Topic with ID {id} not found.");

            _dbContext.Topics.Remove(topic);
            await _dbContext.SaveChangesAsync();
            await _ver.BumpAsync(); // інвалідація кешу
        }
    }
}
