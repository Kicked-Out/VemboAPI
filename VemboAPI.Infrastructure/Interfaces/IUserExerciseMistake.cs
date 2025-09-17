using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserExerciseMistakeService
    {
        Task<List<UserExerciseMistakeDto>> GetAllMistakes();
        Task<UserExerciseMistakeDto> GetMistakeById(int id);
        Task<UserExerciseMistakeDto> CreateMistake(CreateUserExerciseMistakeDto dto);
        Task UpdateMistake(int id, UpdateUserExerciseMistakeDto dto);

        Task DeleteMistake(int id);
    }

}
