using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserExerciseMistakeService
    {
        List<UserExerciseMistakeDto> GetAllMistakes();
        UserExerciseMistakeDto GetMistakeById(int id);
        UserExerciseMistakeDto CreateMistake(int userId, int exerciseId, string userAnswer);
        void UpdateMistake(int id, int userId, int exerciseId, string userAnswer);
        void DeleteMistake(int id);
    }

}
