using System;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IExerciseTypeService
    {
        List<ExerciseTypeDto> GetAllExerciseTypes();
        ExerciseTypeDto GetExerciseTypeById(int id);
        ExerciseTypeDto CreateExerciseType(string title);
        void UpdateExerciseType(int id, string title);
        void DeleteExerciseType(int id);
    }

}

