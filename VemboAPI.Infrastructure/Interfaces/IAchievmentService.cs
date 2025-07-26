using System;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IAchievementService
    {
        Task<List<AchievementDto>> GetAllAsync();
        Task<AchievementDto> GetByIdAsync(int id);
        Task<AchievementDto> CreateAsync(CreateAchievementDto dto);
        Task<AchievementDto> UpdateAsync(int id, UpdateAchievementDto dto);
        Task DeleteAsync(int id);
    }

}

