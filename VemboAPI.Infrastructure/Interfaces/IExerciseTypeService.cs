using System;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IExerciseTypeService
    {
        Task<List<ExerciseTypeDto>> GetAllExerciseTypes();
        Task<ExerciseTypeDto> GetExerciseTypeById(int id);
        Task<ExerciseTypeDto> CreateExerciseType(CreateExerciseTypeDto dto);
        Task UpdateExerciseType(int id, UpdateExerciseTypeDto dto);

        Task DeleteExerciseType(int id);
    }

}

