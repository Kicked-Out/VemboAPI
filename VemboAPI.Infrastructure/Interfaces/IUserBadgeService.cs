using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserBadgeService
    {
        Task<List<UserBadgeDto>> GetAllAsync();
        Task<UserBadgeDto> GetByIdAsync(int id);
        Task<UserBadgeDto> CreateAsync(CreateUserBadgeDto dto);
        Task UpdateAsync(int id, UpdateUserBadgeDto dto);
        Task DeleteAsync(int id);
    }
}
