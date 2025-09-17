using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<UserLessonProgressDto>> GetAllLessonProgress(string userId)
        {
            var progresses = await _dbContext.UserLessonProgresses.Where(lessonProgresses => lessonProgresses.UserId == userId).ToListAsync();

            return _mapper.Map<List<UserLessonProgressDto>>(progresses);
        }

        public async Task<UserLessonProgressDto> GetLessonProgressById(int id)
        {
            var progress = await _dbContext.UserLessonProgresses.FindAsync(id);

            if (progress == null)
                throw new KeyNotFoundException($"Lesson progress with ID {id} not found.");

            return _mapper.Map<UserLessonProgressDto>(progress);
        }

        public async Task<UserLessonProgressDto> CreateLessonProgress(CreateUserLessonProgressDto dto)
        {
            var progress = _mapper.Map<UserLessonProgress>(dto);

            await _dbContext.UserLessonProgresses.AddAsync(progress);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserLessonProgressDto>(progress);
        }


        public async Task UpdateLessonProgress(int id, UpdateUserLessonProgressDto dto)
        {
            var progress = await _dbContext.UserLessonProgresses.FindAsync(id);

            if (progress == null)
                throw new KeyNotFoundException($"Lesson progress with ID {id} not found.");

            _mapper.Map(dto, progress);
            
            await _dbContext.SaveChangesAsync();
        }
        public async Task<UserLessonProgressDto> EnsureProgressExists(string userId, int lessonId)
        {
            var existing = await _dbContext.UserLessonProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.LessonId == lessonId);

            if (existing != null)
                return _mapper.Map<UserLessonProgressDto>(existing);

            var progress = new UserLessonProgress
            {
                UserId = userId,
                LessonId = lessonId,
                CompletedCount = 0
            };

            await _dbContext.UserLessonProgresses.AddAsync(progress);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserLessonProgressDto>(progress);
        }



        public async Task DeleteLessonProgress(int id)
        {
            var progress = await _dbContext.UserLessonProgresses.FindAsync(id);

            if (progress == null)
                throw new KeyNotFoundException($"Lesson progress with ID {id} not found.");

            _dbContext.UserLessonProgresses.Remove(progress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserLessonProgressDto>> GetAllLessonProgressByLevelId(string userId, int levelId)
        {
            var progresses = await _dbContext.UserLessonProgresses
                .Where(lessonProgress => lessonProgress.UserId == userId)
                .Where(lessonProgress => lessonProgress.Lesson.LevelId == levelId)
                .Include(lessonProgress => lessonProgress.Lesson)
                .ToListAsync();

            return _mapper.Map<List<UserLessonProgressDto>>(progresses);
        }

        public async Task<UserLessonProgressDto> GetCurrentLessonProgressByLevelId(string userId, int levelId)
        {
            var progress = await _dbContext.UserLessonProgresses
                .Where(lessonProgress => lessonProgress.UserId == userId)
                .Where(lessonProgress => lessonProgress.Lesson.LevelId == levelId)
                .Include(lessonProgress => lessonProgress.Lesson)
                .OrderBy(lessonProgress => lessonProgress.Id)
                .LastOrDefaultAsync();

            return _mapper.Map<UserLessonProgressDto>(progress);
        }
    }
}
