using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

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

        public List<ExerciseDto> GetAllExercise()
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:exercises:all:v{v}";

            var list = _cache.GetOrSetAsync(key, () =>
            {
                var exercises = _dbContext.Exercises.ToList(); // синхронно ок
                var mapped = _mapper.Map<List<ExerciseDto>>(exercises);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return list;
        }

        public ExerciseDto GetExerciseById(int id)
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:exercise:{id}:v{v}";

            var dto = _cache.GetOrSetAsync(key, () =>
            {
                var exercise = _dbContext.Exercises.Find(id);
                if (exercise == null)
                    throw new KeyNotFoundException($"Exercise with ID {id} not found.");

                var mapped = _mapper.Map<ExerciseDto>(exercise);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return dto!;
        }

        public ExerciseDto CreateExercise(CreateExerciseDto dto)
        {
            var lesson = _dbContext.Lessons.Find(dto.LessonId);
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {dto.LessonId} not found.");

            var exerciseType = _dbContext.ExerciseTypes.Find(dto.ExerciseTypeId);
            if (exerciseType == null)
                throw new KeyNotFoundException($"ExerciseType with ID {dto.ExerciseTypeId} not found.");

            var exercise = _mapper.Map<Exercise>(dto);

            _dbContext.Exercises.Add(exercise);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу через нову версію

            return _mapper.Map<ExerciseDto>(exercise);
        }

        public void UpdateExercise(int id, UpdateExerciseDto dto)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise == null)
                throw new KeyNotFoundException($"Exercise with ID {id} not found.");

            var lesson = _dbContext.Lessons.Find(dto.LessonId);
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {dto.LessonId} not found.");

            var exerciseType = _dbContext.ExerciseTypes.Find(dto.ExerciseTypeId);
            if (exerciseType == null)
                throw new KeyNotFoundException($"ExerciseType with ID {dto.ExerciseTypeId} not found.");

            _mapper.Map(dto, exercise);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }

        public void DeleteExercise(int id)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise == null)
                throw new KeyNotFoundException($"Exercise with ID {id} not found.");

            _dbContext.Exercises.Remove(exercise);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }

        public List<ExerciseDto> GetAllExerciseByLessonId(int lessonId)
        {
            var exercises = _dbContext.Exercises
                .Where(exercise => exercise.LessonId == lessonId)
                .ToList();

            return _mapper.Map<List<ExerciseDto>>(exercises);
        }
    }
}
