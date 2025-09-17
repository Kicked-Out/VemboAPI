using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces; // ICacheService, IContentVersionService
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<LessonDto>> GetAllLessons()
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:lessons:all:v{v}";

            var list = await _cache.GetOrSetAsync(key, async () =>
            {
                var lessons = await _dbContext.Lessons.ToListAsync(); // синхронно ок
                var mapped = _mapper.Map<List<LessonDto>>(lessons);
                
                return mapped;
            }, ttl: null);

            return list;
        }

        public async Task<LessonDto> GetLessonById(int id)
        {
            var v = await _ver.GetVersionAsync();
            var key = $"content:lesson:{id}:v{v}";

            var dto = await _cache.GetOrSetAsync(key, async () =>
            {
                var lesson = await _dbContext.Lessons.FindAsync(id);

                if (lesson == null)
                    throw new KeyNotFoundException($"Lesson with ID {id} not found.");

                var mapped = _mapper.Map<LessonDto>(lesson);
                
                return mapped;
            }, ttl: null);

            return dto!;
        }

        public async Task<LessonDto> CreateLesson(CreateLessonDto dto)
        {
            if (!await _dbContext.Levels.AnyAsync(l => l.Id == dto.LevelId))
                throw new KeyNotFoundException($"Level with ID {dto.LevelId} not found.");

            var lesson = _mapper.Map<Lesson>(dto);
            await _dbContext.Lessons.AddAsync(lesson);
            
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу через нову версію

            return _mapper.Map<LessonDto>(lesson);
        }

        public async Task UpdateLesson(int id, UpdateLessonDto dto)
        {
            var lesson = await _dbContext.Lessons.FindAsync(id);
            
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {id} not found.");

            if (!await _dbContext.Levels.AnyAsync(l => l.Id == dto.LevelId))
                throw new KeyNotFoundException($"Level with ID {dto.LevelId} not found.");

            _mapper.Map(dto, lesson);
            
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task DeleteLesson(int id)
        {
            var lesson = await _dbContext.Lessons.FindAsync(id);
            
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {id} not found.");

            _dbContext.Lessons.Remove(lesson);
            await _dbContext.SaveChangesAsync();

            await _ver.BumpAsync(); // інвалідація кешу
        }

        public async Task<List<LessonDto>> GetAllLessonsByLevelId(int levelId)
        {
            var lessons = await _dbContext.Lessons
                .Where(lesson => lesson.LevelId == levelId)
                .ToListAsync();

            return _mapper.Map<List<LessonDto>>(lessons);
        }
    }
}
