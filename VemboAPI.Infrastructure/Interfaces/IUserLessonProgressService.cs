using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserLessonProgressService
    {
        Task<List<UserLessonProgressDto>> GetAllLessonProgress(string userId);
        Task<List<UserLessonProgressDto>> GetAllLessonProgressByLevelId(string userId, int levelId);
        Task<UserLessonProgressDto> GetLessonProgressById(int id);
        Task<UserLessonProgressDto> GetCurrentLessonProgressByLevelId(string userId, int levelId);
        Task<UserLessonProgressDto> CreateLessonProgress(CreateUserLessonProgressDto dto);
        Task UpdateLessonProgress(int id, UpdateUserLessonProgressDto dto);
        Task<UserLessonProgressDto> EnsureProgressExists(string userId, int lessonId);

        Task DeleteLessonProgress(int id);
    }
}