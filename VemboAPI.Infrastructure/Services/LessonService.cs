using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class LessonService : ILessonService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public LessonService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<LessonDto> GetAllLessons()
        {
            var lessons = _dbContext.Lessons.ToList();
            return _mapper.Map<List<LessonDto>>(lessons);
        }

        public LessonDto GetLessonById(int id)
        {
            var lesson = _dbContext.Lessons.Find(id);
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {id} not found.");

            return _mapper.Map<LessonDto>(lesson);
        }

        public LessonDto CreateLesson(int order, int levelId)
        {
            var level = _dbContext.Levels.Find(levelId);
            if (level == null)
                throw new KeyNotFoundException($"Level with ID {levelId} not found.");

            var lesson = new Lesson
            {
                Order = order,
                LevelId = levelId
            };

            _dbContext.Lessons.Add(lesson);
            _dbContext.SaveChanges();

            return _mapper.Map<LessonDto>(lesson);
        }

        public void UpdateLesson(int id, int order, int levelId)
        {
            var lesson = _dbContext.Lessons.Find(id);
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {id} not found.");

            var level = _dbContext.Levels.Find(levelId);
            if (level == null)
                throw new KeyNotFoundException($"Level with ID {levelId} not found.");

            lesson.Order = order;
            lesson.LevelId = levelId;

            _dbContext.Lessons.Update(lesson);
            _dbContext.SaveChanges();
        }

        public void DeleteLesson(int id)
        {
            var lesson = _dbContext.Lessons.Find(id);
            if (lesson == null)
                throw new KeyNotFoundException($"Lesson with ID {id} not found.");

            _dbContext.Lessons.Remove(lesson);
            _dbContext.SaveChanges();
        }
    }
}
