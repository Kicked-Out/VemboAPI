using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class UserQuestProgressService : IUserQuestProgressService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _versionService;

        public UserQuestProgressService(
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

        public async Task<List<UserQuestProgressDto>> GetAllAsync()
        {
            var version = await _versionService.GetVersionAsync();
            var cacheKey = $"user-quest-progress:all:v{version}";

            return await _cache.GetOrSetAsync(cacheKey, async () =>
            {
                var progresses = await _dbContext.UserQuestProgresses
                    .AsNoTracking()
                    .Include(uq => uq.Quest)
                    .ToListAsync();

                return _mapper.Map<List<UserQuestProgressDto>>(progresses);
            });
        }

        public async Task<UserQuestProgressDto> GetByIdAsync(int id)
        {
            var version = await _versionService.GetVersionAsync();
            var cacheKey = $"user-quest-progress:{id}:v{version}";

            var dto = await _cache.GetOrSetAsync(cacheKey, async () =>
            {
                var progress = await _dbContext.UserQuestProgresses
                    .AsNoTracking()
                    .Include(uq => uq.Quest)
                    .FirstOrDefaultAsync(uq => uq.Id == id);

                if (progress == null)
                {
                    throw new KeyNotFoundException($"User quest progress with ID {id} not found.");
                }

                return _mapper.Map<UserQuestProgressDto>(progress);
            });

            return dto!;
        }

        public async Task<UserQuestProgressDto> GetByQuestId(int questId)
        {
            var version = await _versionService.GetVersionAsync();
            var cacheKey = $"user-quest-progress:quest-id:v{version}";

            var dto = await _cache.GetOrSetAsync(cacheKey, async () =>
            {
                var progress = await _dbContext.UserQuestProgresses
                    .AsNoTracking()
                    .Include(userQuestProgress => userQuestProgress.Quest)
                    .FirstOrDefaultAsync(userQuestProgress => userQuestProgress.QuestId == questId);

                return _mapper.Map<UserQuestProgressDto>(progress);
            });

            return dto;
        }

        public async Task<List<UserQuestProgressDto>> GetAllMonthly()
        {
            var version = await _versionService.GetVersionAsync();
            var cacheKey = $"user-quest-progress:all:monthly:v{version}";

            return await _cache.GetOrSetAsync(cacheKey, async () =>
            {
                var progress = await _dbContext.UserQuestProgresses
                    .AsNoTracking()
                    .Include(userQuestProgress => userQuestProgress.Quest)
                    .ToListAsync();

                return _mapper.Map<List<UserQuestProgressDto>>(progress);
            });
        }

        public async Task<UserQuestProgressDto> CreateAsync(CreateUserQuestProgressDto dto)
        {
            if (!await _dbContext.Users.AnyAsync(u => u.Id == dto.UserId))
            {
                throw new KeyNotFoundException($"User with ID {dto.UserId} not found.");
            }

            if (!await _dbContext.Quests.AnyAsync(q => q.Id == dto.QuestId))
            {
                throw new KeyNotFoundException($"Quest with ID {dto.QuestId} not found.");
            }

            var entity = _mapper.Map<UserQuestProgress>(dto);
            await _dbContext.UserQuestProgresses.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();

            return _mapper.Map<UserQuestProgressDto>(entity);
        }

        public async Task UpdateAsync(int id, UpdateUserQuestProgressDto dto)
        {
            var entity = await _dbContext.UserQuestProgresses.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"User quest progress with ID {id} not found.");
            }

            _mapper.Map(dto, entity);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.UserQuestProgresses.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"User quest progress with ID {id} not found.");
            }

            _dbContext.UserQuestProgresses.Remove(entity);
            await _dbContext.SaveChangesAsync();
            await _versionService.BumpAsync();
        }
    }
}
