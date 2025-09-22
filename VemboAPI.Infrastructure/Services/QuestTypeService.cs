using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class QuestTypeService : IQuestTypeService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _versionService;

        public QuestTypeService(
            VemboDbContext dbContext,
            IMapper mapper,
            ICacheService cache,
            IContentVersionService versionService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
            _versionService = versionService;
        }

        public async Task<List<QuestTypeDto>> GetAllAsync()
        {
            var version = await _versionService.GetVersionAsync();
            var cacheKey = $"content:quest-types:all:v{version}";

            return await _cache.GetOrSetAsync(cacheKey, async () =>
            {
                var questTypes = await _dbContext.QuestTypes
                    .AsNoTracking()
                    .ToListAsync();

                return _mapper.Map<List<QuestTypeDto>>(questTypes);
            });
        }

        public async Task<QuestTypeDto> GetByIdAsync(int id)
        {
            var version = await _versionService.GetVersionAsync();
            var cacheKey = $"content:quest-type:{id}:v{version}";

            var dto = await _cache.GetOrSetAsync(cacheKey, async () =>
            {
                var questType = await _dbContext.QuestTypes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(qt => qt.Id == id);

                if (questType == null)
                {
                    throw new KeyNotFoundException($"Quest type with ID {id} not found.");
                }

                return _mapper.Map<QuestTypeDto>(questType);
            });

            return dto!;
        }

        public async Task<QuestTypeDto> CreateAsync(CreateQuestTypeDto dto)
        {
            var questType = _mapper.Map<QuestType>(dto);
            await _dbContext.QuestTypes.AddAsync(questType);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();

            return _mapper.Map<QuestTypeDto>(questType);
        }

        public async Task UpdateAsync(int id, UpdateQuestTypeDto dto)
        {
            var questType = await _dbContext.QuestTypes.FindAsync(id);
            if (questType == null)
            {
                throw new KeyNotFoundException($"Quest type with ID {id} not found.");
            }

            _mapper.Map(dto, questType);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var questType = await _dbContext.QuestTypes.FindAsync(id);
            if (questType == null)
            {
                throw new KeyNotFoundException($"Quest type with ID {id} not found.");
            }

            var isInUse = await _dbContext.Quests.AnyAsync(q => q.QuestTypeId == id);
            if (isInUse)
            {
                throw new InvalidOperationException("Cannot delete quest type that is used by quests.");
            }

            _dbContext.QuestTypes.Remove(questType);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();
        }
    }
}
