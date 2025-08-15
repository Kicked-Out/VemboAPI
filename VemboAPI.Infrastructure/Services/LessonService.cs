using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class LessonService : ILessonService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IContentVersionService _ver;

        public LessonService(
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

        public List<LessonDto> GetAllLessons()
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:lessons:all:v{v}";

            var list = _cache.GetOrSetAsync(key, () =>
            {
                var lessons = _dbContext.Lessons.ToList(); // синхронно ок
                var mapped = _mapper.Map<List<LessonDto>>(lessons);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return list;
        }

        public LessonDto GetLessonById(int id)
        {
            var v = _ver.GetVersionAsync().GetAwaiter().GetResult();
            var key = $"content:lesson:{id}:v{v}";

            var dto = _cache.GetOrSetAsync(key, () =>
            {
                var lesson = _dbContext.Lessons.Find(id);
                if (lesson == null)
                    throw new KeyNotFoundException($"Lesson with ID {id} not found.");

                var mapped = _mapper.Map<LessonDto>(lesson);
                return Task.FromResult(mapped);
            }, ttl: null).GetAwaiter().GetResult();

            return dto!;
        }

        public LessonDto CreateLesson(CreateLessonDto dto)
        {
            if (!_dbContext.Levels.Any(l => l.Id == dto.LevelId))
                throw new KeyNotFoundException($"Level with ID {dto.LevelId} not found.");

            var lesson = _mapper.Map<Lesson>(dto);
            _dbContext.Lessons.Add(lesson);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу через нову версію

            return _mapper.Map<LessonDto>(lesson);
        }

        public void UpdateLesson(int id, UpdateLessonDto dto)
        {
            var lesson = _dbContext.Lessons.Find(id);
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {id} not found.");

            if (!_dbContext.Levels.Any(l => l.Id == dto.LevelId))
                throw new KeyNotFoundException($"Level with ID {dto.LevelId} not found.");

            _mapper.Map(dto, lesson);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }

        public void DeleteLesson(int id)
        {
            var lesson = _dbContext.Lessons.Find(id);
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {id} not found.");

            _dbContext.Lessons.Remove(lesson);
            _dbContext.SaveChanges();

            _ver.BumpAsync().GetAwaiter().GetResult(); // інвалідація кешу
        }
    }
}
