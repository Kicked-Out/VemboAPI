using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class ExerciseTypeService : IExerciseTypeService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public ExerciseTypeService(
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

        public List<ExerciseTypeDto> GetAllExerciseTypes()
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:exercise-types:all:v{v}";

            var list = _cache.GetOrSetAsync(key, () =>
            {
                var items = _dbContext.ExerciseTypes.ToList(); // синхронно ок
                var mapped = _mapper.Map<List<ExerciseTypeDto>>(items);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return list;
        }

        public ExerciseTypeDto GetExerciseTypeById(int id)
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:exercise-type:{id}:v{v}";

            var dto = _cache.GetOrSetAsync(key, () =>
            {
                var entity = _dbContext.ExerciseTypes.Find(id);
                if (entity == null)
                    throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");

                var mapped = _mapper.Map<ExerciseTypeDto>(entity);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return dto!;
        }

        public ExerciseTypeDto CreateExerciseType(CreateExerciseTypeDto dto)
        {
            var entity = _mapper.Map<ExerciseType>(dto);

            _dbContext.ExerciseTypes.Add(entity);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу

            return _mapper.Map<ExerciseTypeDto>(entity);
        }

        public void UpdateExerciseType(int id, UpdateExerciseTypeDto dto)
        {
            var entity = _dbContext.ExerciseTypes.Find(id);
            if (entity == null)
                throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");

            _mapper.Map(dto, entity);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }

        public void DeleteExerciseType(int id)
        {
            var entity = _dbContext.ExerciseTypes.Find(id);
            if (entity == null)
                throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");

            _dbContext.ExerciseTypes.Remove(entity);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }
    }
}
