using System;
using VemboAPI.Domain.DTO;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserStatisticService
    {
        Task<List<UserStatisticDto>> GetAllAsync();
        Task<UserStatisticDto> GetByIdAsync(int id);
        Task<UserStatisticDto> GetByUserId(string userId);
        Task<UserStatisticDto> CreateAsync(CreateUserStatisticDto dto);
        Task<UserStatisticDto> GetByUserIdAsync(string userId);
        Task UpdateAsync(int id, UpdateUserStatisticDto dto);
        Task UpdateTotalXP(string userId, UpdateUserTotalXPDto dto);
        Task UpdateCoins(string userId, UpdateUserCoinsDto dto);
        Task DeleteAsync(int id);
    }

}

