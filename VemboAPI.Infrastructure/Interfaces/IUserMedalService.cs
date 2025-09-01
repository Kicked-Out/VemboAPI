using System.Collections.Generic;
using System.Threading.Tasks;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserMedalService
    {
        Task<List<UserMedalDto>> GetAllAsync();
        Task<UserMedalDto> GetByIdAsync(int id);
        Task<UserMedalDto> CreateAsync(CreateUserMedalDto dto);
        Task UpdateAsync(int id, UpdateUserMedalDto dto);
        Task DeleteAsync(int id);
    }
}
