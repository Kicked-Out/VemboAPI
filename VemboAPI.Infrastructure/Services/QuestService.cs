using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class QuestService : IQuestService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _versionService;

        public QuestService(
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

        public async Task<List<QuestDto>> GetAllAsync()
        {
            var version = await _versionService.GetVersionAsync();
            var cacheKey = $"content:quests:all:v{version}";

            return await _cache.GetOrSetAsync(cacheKey, async () =>
            {
                var quests = await _dbContext.Quests
                    .AsNoTracking()
                    .Include(q => q.QuestDefinition)
                    .Include(q => q.QuestType)
                    .ToListAsync();

                return _mapper.Map<List<QuestDto>>(quests);
            });
        }

        public async Task<QuestDto> GetByIdAsync(int id)
        {
            var version = await _versionService.GetVersionAsync();
            var cacheKey = $"content:quest:{id}:v{version}";

            var dto = await _cache.GetOrSetAsync(cacheKey, async () =>
            {
                var quest = await _dbContext.Quests
                    .AsNoTracking()
                    .Include(q => q.QuestDefinition)
                    .Include(q => q.QuestType)
                    .FirstOrDefaultAsync(q => q.Id == id);

                if (quest == null)
                {
                    throw new KeyNotFoundException($"Quest with ID {id} not found.");
                }

                return _mapper.Map<QuestDto>(quest);
            });

            return dto!;
        }

        public async Task<QuestDto> CreateAsync(CreateQuestDto dto)
        {
            if (!await _dbContext.QuestDefinitions.AnyAsync(q => q.Id == dto.QuestDefinitionId))
            {
                throw new KeyNotFoundException($"Quest definition with ID {dto.QuestDefinitionId} not found.");
            }

            if (!await _dbContext.QuestTypes.AnyAsync(qt => qt.Id == dto.QuestTypeId))
            {
                throw new KeyNotFoundException($"Quest type with ID {dto.QuestTypeId} not found.");
            }

            var quest = _mapper.Map<Quest>(dto);
            await _dbContext.Quests.AddAsync(quest);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();

            return _mapper.Map<QuestDto>(quest);
        }

        public async Task UpdateAsync(int id, UpdateQuestDto dto)
        {
            var quest = await _dbContext.Quests.FindAsync(id);
            if (quest == null)
            {
                throw new KeyNotFoundException($"Quest with ID {id} not found.");
            }

            if (!await _dbContext.QuestDefinitions.AnyAsync(q => q.Id == dto.QuestDefinitionId))
            {
                throw new KeyNotFoundException($"Quest definition with ID {dto.QuestDefinitionId} not found.");
            }

            if (!await _dbContext.QuestTypes.AnyAsync(qt => qt.Id == dto.QuestTypeId))
            {
                throw new KeyNotFoundException($"Quest type with ID {dto.QuestTypeId} not found.");
            }

            _mapper.Map(dto, quest);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var quest = await _dbContext.Quests.FindAsync(id);
            if (quest == null)
            {
                throw new KeyNotFoundException($"Quest with ID {id} not found.");
            }

            _dbContext.Quests.Remove(quest);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();
        }
    }
}
