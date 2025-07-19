using System;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IExerciseTypeService
    {
        List<ExerciseTypeDto> GetAllExerciseTypes();
        ExerciseTypeDto GetExerciseTypeById(int id);
        ExerciseTypeDto CreateExerciseType(CreateExerciseTypeDto dto);
        void UpdateExerciseType(int id, UpdateExerciseTypeDto dto);

        void DeleteExerciseType(int id);
    }

}

