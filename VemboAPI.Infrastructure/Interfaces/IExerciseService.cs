using System;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IExerciseService
    {
        Task<List<ExerciseDto>> GetAllExercise();
        Task<List<ExerciseDto>> GetAllExerciseByLessonId(int lessonId);
        Task<ExerciseDto> GetExerciseById(int id);
        Task<ExerciseDto> CreateExercise(CreateExerciseDto dto);
        Task UpdateExercise(int id, UpdateExerciseDto dto);

        Task DeleteExercise(int id);
    }

}

