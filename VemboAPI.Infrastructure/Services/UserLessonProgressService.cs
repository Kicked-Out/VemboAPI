using System.Collections.Generic;
using System.Linq;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Services
{
    public class UserLessonProgressService : IUserLessonProgressService
    {
        private readonly VemboDbContext _dbContext;

        public UserLessonProgressService(VemboDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserLessonProgressDto> GetAllLessonProgress()
        {
            return _dbContext.UserLessonProgresses
                .Select(lp => new UserLessonProgressDto
                {
                    Id = lp.Id,
                    UserId = lp.UserId,
                    LessonId = lp.LessonId,
                    isCompleted = lp.isCompleted
                })
                .ToList();
        }

        public UserLessonProgressDto GetLessonProgressById(int id)
        {
            var progress = _dbContext.UserLessonProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"Lesson progress with ID {id} not found.");

            return new UserLessonProgressDto
            {
                Id = progress.Id,
                UserId = progress.UserId,
                LessonId = progress.LessonId,
                isCompleted = progress.isCompleted
            };
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

            return new UserLessonProgressDto
            {
                Id = progress.Id,
                UserId = progress.UserId,
                LessonId = progress.LessonId,
                isCompleted = progress.isCompleted
            };
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
