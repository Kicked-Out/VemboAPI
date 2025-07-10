using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserLessonProgressService
    {
        List<UserLessonProgressDto> GetAllLessonProgress();
        UserLessonProgressDto GetLessonProgressById(int id);
        UserLessonProgressDto CreateLessonProgress(int userId, int lessonId, bool isCompleted);
        void UpdateLessonProgress(int id, int userId, int lessonId, bool isCompleted);
        void DeleteLessonProgress(int id);
    }
}
