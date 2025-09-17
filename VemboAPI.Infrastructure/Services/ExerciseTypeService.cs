using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<ExerciseTypeDto>> GetAllExerciseTypes()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:exercise-types:all:v{v}";

            var list = await _cache.GetOrSetAsync(key, async () =>
            {
                var items = await _dbContext.ExerciseTypes.ToListAsync(); // синхронно ок
                var mapped = _mapper.Map<List<ExerciseTypeDto>>(items);
                
                return mapped;
            }, ttl: null);

            return list;
        }

        public async Task<ExerciseTypeDto> GetExerciseTypeById(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:exercise-type:{id}:v{v}";

            var dto = await _cache.GetOrSetAsync(key, async () =>
            {
                var entity = await _dbContext.ExerciseTypes.FindAsync(id);
                
                if (entity == null)
                    throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");

                var mapped = _mapper.Map<ExerciseTypeDto>(entity);
                
                return mapped;
            }, ttl: null);

            return dto!;
        }

        public async Task<ExerciseTypeDto> CreateExerciseType(CreateExerciseTypeDto dto)
        {
            var entity = _mapper.Map<ExerciseType>(dto);

            await _dbContext.ExerciseTypes.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу

            return _mapper.Map<ExerciseTypeDto>(entity);
        }

        public async Task UpdateExerciseType(int id, UpdateExerciseTypeDto dto)
        {
            var entity = await _dbContext.ExerciseTypes.FindAsync(id);
            
            if (entity == null)
                throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");

            _mapper.Map(dto, entity);

            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task DeleteExerciseType(int id)
        {
            var entity = await _dbContext.ExerciseTypes.FindAsync(id);

            if (entity == null)
                throw new KeyNotFoundException($"ExerciseType with ID {id} not found.");

            _dbContext.ExerciseTypes.Remove(entity);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }
    }
}
