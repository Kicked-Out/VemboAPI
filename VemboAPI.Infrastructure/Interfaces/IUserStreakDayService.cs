using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserStreakDayService
    {
        Task<List<UserStreakDayDto>> GetAllAsync();
        Task<UserStreakDayDto> GetByIdAsync(int id);
        Task<UserStreakDayDto> CreateAsync(CreateUserStreakDayDto dto);
        Task UpdateAsync(int id, UpdateUserStreakDayDto dto);
        Task DeleteAsync(int id);
    }
}
