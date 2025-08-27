using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserExerciseMistakeService
    {
        List<UserExerciseMistakeDto> GetAllMistakes();
        UserExerciseMistakeDto GetMistakeById(int id);
        UserExerciseMistakeDto CreateMistake(CreateUserExerciseMistakeDto dto);
        void UpdateMistake(int id, UpdateUserExerciseMistakeDto dto);

        void DeleteMistake(int id);
    }

}
