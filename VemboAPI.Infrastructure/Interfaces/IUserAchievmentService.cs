using System;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserAchievementService
    {
        Task<List<UserAchievementDto>> GetAllAsync();
        Task<UserAchievementDto> GetByIdAsync(int id);
        Task<UserAchievementDto> CreateAsync(CreateUserAchievementDto dto);
        Task UpdateAsync(int id, UpdateUserAchievementDto dto);
        Task DeleteAsync(int id);
    }

}

