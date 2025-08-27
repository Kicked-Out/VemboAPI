using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserLessonProgressService
    {
        List<UserLessonProgressDto> GetAllLessonProgress(string userId);
        List<UserLessonProgressDto> GetAllLessonProgressByLevelId(string userId, int levelId);
        UserLessonProgressDto GetLessonProgressById(int id);
        UserLessonProgressDto GetCurrentLessonProgressByLevelId(string userId, int levelId);
        UserLessonProgressDto CreateLessonProgress(CreateUserLessonProgressDto dto);
        void UpdateLessonProgress(int id, UpdateUserLessonProgressDto dto);
        UserLessonProgressDto EnsureProgressExists(int userId, int lessonId);

        void DeleteLessonProgress(int id);
    }
}