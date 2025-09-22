using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class QuestDefinitionService : IQuestDefinitionService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _versionService;

        public QuestDefinitionService(
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

        public async Task<List<QuestDefinitionDto>> GetAllAsync()
        {
            var version = await _versionService.GetVersionAsync();
            var cacheKey = $"content:quest-definitions:all:v{version}";

            return await _cache.GetOrSetAsync(cacheKey, async () =>
            {
                var quests = await _dbContext.QuestDefinitions
                    .AsNoTracking()
                    .ToListAsync();

                return _mapper.Map<List<QuestDefinitionDto>>(quests);
            });
        }

        public async Task<QuestDefinitionDto> GetByIdAsync(int id)
        {
            var version = await _versionService.GetVersionAsync();
            var cacheKey = $"content:quest-definition:{id}:v{version}";

            var dto = await _cache.GetOrSetAsync(cacheKey, async () =>
            {
                var quest = await _dbContext.QuestDefinitions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(q => q.Id == id);

                if (quest == null)
                {
                    throw new KeyNotFoundException($"Quest definition with ID {id} not found.");
                }

                return _mapper.Map<QuestDefinitionDto>(quest);
            });

            return dto!;
        }

        public async Task<QuestDefinitionDto> CreateAsync(CreateQuestDefinitionDto dto)
        {
            var quest = _mapper.Map<QuestDefinition>(dto);
            await _dbContext.QuestDefinitions.AddAsync(quest);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();

            return _mapper.Map<QuestDefinitionDto>(quest);
        }

        public async Task UpdateAsync(int id, UpdateQuestDefinitionDto dto)
        {
            var quest = await _dbContext.QuestDefinitions.FindAsync(id);
            if (quest == null)
            {
                throw new KeyNotFoundException($"Quest definition with ID {id} not found.");
            }

            _mapper.Map(dto, quest);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var quest = await _dbContext.QuestDefinitions.FindAsync(id);
            if (quest == null)
            {
                throw new KeyNotFoundException($"Quest definition with ID {id} not found.");
            }

            _dbContext.QuestDefinitions.Remove(quest);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();
        }
    }
}
