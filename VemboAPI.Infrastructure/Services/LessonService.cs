using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace VemboAPI.Infrastructure.Services
{
    public class LessonService : ILessonService
    {
        private readonly VemboDbContext _dbContext;

        public LessonService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<LessonDto> GetAllLessons()
        {
            return _dbContext.Lessons
                .Select(l => new LessonDto
                {
                    Id = l.Id,
                    Order = l.Order,
                    LevelId = l.LevelId
                })
                .ToList();
        }

        public LessonDto GetLessonById(int id)
        {
            var lesson = _dbContext.Lessons.Find(id);
            if (lesson == null)
            {
                throw new KeyNotFoundException($"Lesson with ID {id} not found.");
            }

            return new LessonDto
            {
                Id = lesson.Id,
                Order = lesson.Order,
                LevelId = lesson.LevelId
            };
        }

        public LessonDto CreateLesson(int order, int levelId)
        {
            var level = _dbContext.Levels.Find(levelId);
            if (level == null)
            {
                throw new KeyNotFoundException($"Level with ID {levelId} not found.");
            }

            var lesson = new Lesson
            {
                
                Order = order,
                LevelId = levelId
            };

            _dbContext.Lessons.Add(lesson);
            _dbContext.SaveChanges();

            return new LessonDto
            {
                Id = lesson.Id,
                Order = lesson.Order,
                LevelId = lesson.LevelId
            };
        }

        public void UpdateLesson(int id, int order, int levelId)
        {
            var lesson = _dbContext.Lessons.Find(id);
            if (lesson == null)
            {
                throw new KeyNotFoundException($"Lesson with ID {id} not found.");
            }

            var level = _dbContext.Levels.Find(levelId);
            if (level == null)
            {
                throw new KeyNotFoundException($"Level with ID {levelId} not found.");
            }

           
            lesson.Order = order;
            lesson.LevelId = levelId;

            _dbContext.Lessons.Update(lesson);
            _dbContext.SaveChanges();
        }

        public void DeleteLesson(int id)
        {
            var lesson = _dbContext.Lessons.Find(id);
            if (lesson == null)
            {
                throw new KeyNotFoundException($"Lesson with ID {id} not found.");
            }

            _dbContext.Lessons.Remove(lesson);
            _dbContext.SaveChanges();
        }
    }
}
