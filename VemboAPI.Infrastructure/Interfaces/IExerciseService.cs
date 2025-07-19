using System;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IExerciseService
    {
        List<ExerciseDto> GetAllExercise();
        ExerciseDto GetExerciseById(int id);
        ExerciseDto CreateExercise(CreateExerciseDto dto);
        void UpdateExercise(int id, UpdateExerciseDto dto);

        void DeleteExercise(int id);
    }

}

