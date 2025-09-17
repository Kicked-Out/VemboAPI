using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class LevelTypeService : ILevelTypeService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public LevelTypeService(
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

        public async Task<List<LevelTypeDto>> GetAll()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:leveltypes:all:v{v}";

            var list = await _cache.GetOrSetAsync(key, async () =>
            {
                var items = await _dbContext.LevelTypes.ToListAsync(); // синхронно ок
                
                var mapped = _mapper.Map<List<LevelTypeDto>>(items);
                
                return mapped;
            }, ttl: null);

            return list;
        }

        public async Task<LevelTypeDto> GetById(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:leveltype:{id}:v{v}";

            var dto = await _cache.GetOrSetAsync(key, async () =>
            {
                var entity = await _dbContext.LevelTypes.FindAsync(id);

                if (entity == null)
                    throw new KeyNotFoundException($"LevelType with ID {id} not found.");

                var mapped = _mapper.Map<LevelTypeDto>(entity);
                
                return mapped;
            }, ttl: null);

            return dto!;
        }

        public async Task<LevelTypeDto> Create(CreateLevelTypeDto dto)
        {
            var entity = _mapper.Map<LevelType>(dto);
            await _dbContext.LevelTypes.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу

            return _mapper.Map<LevelTypeDto>(entity);
        }

        public async Task Update(int id, UpdateLevelTypeDto dto)
        {
            var entity = await _dbContext.LevelTypes.FindAsync(id);
            
            if (entity == null)
                throw new KeyNotFoundException($"LevelType with ID {id} not found.");

            _mapper.Map(dto, entity);

            await _dbContext.SaveChangesAsync();
            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task Delete(int id)
        {
            var entity = await _dbContext.LevelTypes.FindAsync(id);

            if (entity == null)
                throw new KeyNotFoundException($"LevelType with ID {id} not found.");

            _dbContext.LevelTypes.Remove(entity);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }
    }
}
