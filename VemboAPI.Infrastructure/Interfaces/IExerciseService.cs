using System;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IExerciseService
    {
        List<ExerciseDto> GetAllExercise();
        ExerciseDto GetExerciseById(int id);
        ExerciseDto CreateExercise(string title, int lessonId, bool difficulty, int exerciseTypeId, int order);
        void UpdateExercise(int id, string title, int lessonId, bool difficulty, int exerciseTypeId, int order);
        void DeleteExercise(int id);
    }

}

