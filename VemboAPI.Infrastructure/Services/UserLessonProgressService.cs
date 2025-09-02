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

        public List<UserLessonProgressDto> GetAllLessonProgress(string userId)
        {
            var progresses = _dbContext.UserLessonProgresses.ToList().FindAll(lessonProgresses => lessonProgresses.UserId == userId);
            return _mapper.Map<List<UserLessonProgressDto>>(progresses);
        }

        public UserLessonProgressDto GetLessonProgressById(int id)
        {
            var progress = _dbContext.UserLessonProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"Lesson progress with ID {id} not found.");

            return _mapper.Map<UserLessonProgressDto>(progress);
        }

        public UserLessonProgressDto CreateLessonProgress(CreateUserLessonProgressDto dto)
        {
            var progress = _mapper.Map<UserLessonProgress>(dto);

            _dbContext.UserLessonProgresses.Add(progress);
            _dbContext.SaveChanges();

            return _mapper.Map<UserLessonProgressDto>(progress);
        }


        public void UpdateLessonProgress(int id, UpdateUserLessonProgressDto dto)
        {
            var progress = _dbContext.UserLessonProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"Lesson progress with ID {id} not found.");

            _mapper.Map(dto, progress);
            _dbContext.SaveChanges();
        }
        public UserLessonProgressDto EnsureProgressExists(string userId, int lessonId)
        {
            var existing = _dbContext.UserLessonProgresses
                .FirstOrDefault(p => p.UserId == userId && p.LessonId == lessonId);

            if (existing != null)
                return _mapper.Map<UserLessonProgressDto>(existing);

            var progress = new UserLessonProgress
            {
                UserId = userId,
                LessonId = lessonId,
                CompletedCount = 0
            };

            _dbContext.UserLessonProgresses.Add(progress);
            _dbContext.SaveChanges();

            return _mapper.Map<UserLessonProgressDto>(progress);
        }



        public void DeleteLessonProgress(int id)
        {
            var progress = _dbContext.UserLessonProgresses.Find(id);
            if (progress == null)
                throw new KeyNotFoundException($"Lesson progress with ID {id} not found.");

            _dbContext.UserLessonProgresses.Remove(progress);
            _dbContext.SaveChanges();
        }

        public List<UserLessonProgressDto> GetAllLessonProgressByLevelId(string userId, int levelId)
        {
            var progresses = _dbContext.UserLessonProgresses
                .ToList()
                .FindAll(lessonProgress => lessonProgress.UserId == userId)
                .FindAll(lessonProgress => lessonProgress.Id == levelId);

            return _mapper.Map<List<UserLessonProgressDto>>(progresses);
        }

        public UserLessonProgressDto GetCurrentLessonProgressByLevelId(string userId, int levelId)
        {
            var progress = _dbContext.UserLessonProgresses
                .ToList()
                .FindAll(lessonProgress => lessonProgress.UserId == userId)
                .FindAll(lessonProgress => lessonProgress.Id == levelId)
                .LastOrDefault();

            return _mapper.Map<UserLessonProgressDto>(progress);
        }
    }
}
