using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserStreakService
    {
        Task<List<UserStreakDto>> GetAllAsync();
        Task<UserStreakDto> GetByIdAsync(int id);
        Task<UserStreakDto> CreateAsync(CreateUserStreakDto dto);
        Task UpdateAsync(int id, UpdateUserStreakDto dto);
        Task DeleteAsync(int id);
    }
}
