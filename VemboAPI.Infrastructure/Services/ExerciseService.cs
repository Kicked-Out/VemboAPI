using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public ExerciseService(
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

        public async Task<List<ExerciseDto>> GetAllExercise()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:exercises:all:v{v}";

            var list = await _cache.GetOrSetAsync(key, async () =>
            {
                var exercises = await _dbContext.Exercises.ToListAsync(); // синхронно ок
                
                var mapped = _mapper.Map<List<ExerciseDto>>(exercises);
                
                return mapped;
            }, ttl: null);

            return list;
        }

        public async Task<ExerciseDto> GetExerciseById(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:exercise:{id}:v{v}";

            var dto = await _cache.GetOrSetAsync(key, async () =>
            {
                var exercise = await _dbContext.Exercises.FindAsync(id);

                if (exercise == null)
                    throw new KeyNotFoundException($"Exercise with ID {id} not found.");

                var mapped = _mapper.Map<ExerciseDto>(exercise);
                
                return mapped;
            }, ttl: null);

            return dto!;
        }

        public async Task<ExerciseDto> CreateExercise(CreateExerciseDto dto)
        {
            var lesson = await _dbContext.Lessons.FindAsync(dto.LessonId);

            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {dto.LessonId} not found.");

            var exerciseType = await _dbContext.ExerciseTypes.FindAsync(dto.ExerciseTypeId);
            
            if (exerciseType == null)
                throw new KeyNotFoundException($"ExerciseType with ID {dto.ExerciseTypeId} not found.");

            var exercise = _mapper.Map<Exercise>(dto);

            await _dbContext.Exercises.AddAsync(exercise);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу через нову версію

            return _mapper.Map<ExerciseDto>(exercise);
        }

        public async Task UpdateExercise(int id, UpdateExerciseDto dto)
        {
            var exercise = await _dbContext.Exercises.FindAsync(id);
            
            if (exercise == null)
                throw new KeyNotFoundException($"Exercise with ID {id} not found.");

            var lesson = await _dbContext.Lessons.FindAsync(dto.LessonId);
            
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {dto.LessonId} not found.");

            var exerciseType = await _dbContext.ExerciseTypes.FindAsync(dto.ExerciseTypeId);
            
            if (exerciseType == null)
                throw new KeyNotFoundException($"ExerciseType with ID {dto.ExerciseTypeId} not found.");

            _mapper.Map(dto, exercise);
            
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task DeleteExercise(int id)
        {
            var exercise = await _dbContext.Exercises.FindAsync(id);

            if (exercise == null)
                throw new KeyNotFoundException($"Exercise with ID {id} not found.");

            _dbContext.Exercises.Remove(exercise);
            
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task<List<ExerciseDto>> GetAllExerciseByLessonId(int lessonId)
        {
            var exercises = await _dbContext.Exercises
                .Where(exercise => exercise.LessonId == lessonId)
                .ToListAsync();

            return _mapper.Map<List<ExerciseDto>>(exercises);
        }
    }
}
