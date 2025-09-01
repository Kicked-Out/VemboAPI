using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using AutoMapper;

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

        public List<LevelTypeDto> GetAll()
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:leveltypes:all:v{v}";

            var list = _cache.GetOrSetAsync(key, () =>
            {
                var items = _dbContext.LevelTypes.ToList(); // синхронно ок
                var mapped = _mapper.Map<List<LevelTypeDto>>(items);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return list;
        }

        public LevelTypeDto GetById(int id)
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:leveltype:{id}:v{v}";

            var dto = _cache.GetOrSetAsync(key, () =>
            {
                var entity = _dbContext.LevelTypes.Find(id);
                if (entity == null)
                    throw new KeyNotFoundException($"LevelType with ID {id} not found.");

                var mapped = _mapper.Map<LevelTypeDto>(entity);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return dto!;
        }

        public LevelTypeDto Create(CreateLevelTypeDto dto)
        {
            var entity = _mapper.Map<LevelType>(dto);
            _dbContext.LevelTypes.Add(entity);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу

            return _mapper.Map<LevelTypeDto>(entity);
        }

        public void Update(int id, UpdateLevelTypeDto dto)
        {
            var entity = _dbContext.LevelTypes.Find(id);
            if (entity == null)
                throw new KeyNotFoundException($"LevelType with ID {id} not found.");

            _mapper.Map(dto, entity);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }

        public void Delete(int id)
        {
            var entity = _dbContext.LevelTypes.Find(id);
            if (entity == null)
                throw new KeyNotFoundException($"LevelType with ID {id} not found.");

            _dbContext.LevelTypes.Remove(entity);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }
    }
}
