using System;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IAchievementLevelService
    {
        Task<List<AchievementLevelDto>> GetAllAsync();
        Task<AchievementLevelDto> GetByIdAsync(int id);
        Task<AchievementLevelDto> GetByAchievementIdAndLevelId(int achievementId, int levelId);
        Task<bool> CheckIsNextLevel(int achievementId, int currentLevel);
        Task<AchievementLevelDto> CreateAsync(CreateAchievementLevelDto dto);
        Task UpdateAsync(int id, UpdateAchievementLevelDto dto);
        Task DeleteAsync(int id);
    }

}

