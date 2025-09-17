using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface ILessonService
    {
        Task<List<LessonDto>> GetAllLessons();
        Task<List<LessonDto>> GetAllLessonsByLevelId(int levelId);
        Task<LessonDto> GetLessonById(int id);
        Task<LessonDto> CreateLesson(CreateLessonDto dto);
        Task UpdateLesson(int id, UpdateLessonDto dto);

        Task DeleteLesson(int id);
    }
}
