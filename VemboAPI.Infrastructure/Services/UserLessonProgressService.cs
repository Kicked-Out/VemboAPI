using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class UserLessonProgressService : IUserLessonProgressService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserLessonProgressService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<UserLessonProgressDto> GetAllLessonProgress()
        {
            var progresses = _dbContext.UserLessonProgresses.ToList();
            return _mapper.Map<List<UserLessonProgressDto>>(progresses);
        }

        public UserLessonProgressDto GetLessonProgressById(int id)
        {
            var progress = _dbContext.UserLessonProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"Lesson progress with ID {id} not found.");

            return _mapper.Map<UserLessonProgressDto>(progress);
        }

        public UserLessonProgressDto CreateLessonProgress(int userId, int lessonId, bool isCompleted)
        {
            var progress = new UserLessonProgress
            {
                UserId = userId,
                LessonId = lessonId,
                isCompleted = isCompleted
            };

            _dbContext.UserLessonProgresses.Add(progress);
            _dbContext.SaveChanges();

            return _mapper.Map<UserLessonProgressDto>(progress);
        }

        public void UpdateLessonProgress(int id, int userId, int lessonId, bool isCompleted)
        {
            var progress = _dbContext.UserLessonProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"Lesson progress with ID {id} not found.");

            progress.UserId = userId;
            progress.LessonId = lessonId;
            progress.isCompleted = isCompleted;

            _dbContext.UserLessonProgresses.Update(progress);
            _dbContext.SaveChanges();
        }

        public void DeleteLessonProgress(int id)
        {
            var progress = _dbContext.UserLessonProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"Lesson progress with ID {id} not found.");

            _dbContext.UserLessonProgresses.Remove(progress);
            _dbContext.SaveChanges();
        }
    }
}
