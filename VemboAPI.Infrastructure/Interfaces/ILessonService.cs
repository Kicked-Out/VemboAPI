using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface ILessonService
    {
        List<LessonDto> GetAllLessons();
        LessonDto GetLessonById(int id);
        LessonDto CreateLesson(int order, int levelId);
        void UpdateLesson(int id, int order, int levelId);
        void DeleteLesson(int id);
    }
}
