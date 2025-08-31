using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IBadgeService
    {
        Task<List<BadgeDto>> GetAllAsync();
        Task<BadgeDto> GetByIdAsync(int id);
        Task<BadgeDto> CreateAsync(CreateBadgeDto dto);
        Task<BadgeDto> UpdateAsync(int id, UpdateBadgeDto dto);
        Task DeleteAsync(int id);
    }
}
