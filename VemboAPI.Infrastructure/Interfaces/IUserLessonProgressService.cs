using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserLessonProgressService
    {
        List<UserLessonProgressDto> GetAllLessonProgress();
        UserLessonProgressDto GetLessonProgressById(int id);
        UserLessonProgressDto CreateLessonProgress(CreateUserLessonProgressDto dto);
        void UpdateLessonProgress(int id, UpdateUserLessonProgressDto dto);
        UserLessonProgressDto EnsureProgressExists(int userId, int lessonId);

        void DeleteLessonProgress(int id);
    }
}
