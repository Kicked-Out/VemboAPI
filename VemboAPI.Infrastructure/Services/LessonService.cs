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

        public LessonDto CreateLesson(CreateLessonDto dto)
        {
            if (!_dbContext.Levels.Any(l => l.Id == dto.LevelId))
                throw new KeyNotFoundException($"Level with ID {dto.LevelId} not found.");

            var lesson = _mapper.Map<Lesson>(dto);
            _dbContext.Lessons.Add(lesson);
            _dbContext.SaveChanges();

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
