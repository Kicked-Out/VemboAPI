using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface ILessonService
    {
        List<LessonDto> GetAllLessons();
        List<LessonDto> GetAllLessonsByLevelId(int levelId);
        LessonDto GetLessonById(int id);
        LessonDto CreateLesson(CreateLessonDto dto);
        void UpdateLesson(int id, UpdateLessonDto dto);

        void DeleteLesson(int id);
    }
}
