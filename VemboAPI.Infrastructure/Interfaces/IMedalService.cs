using System.Collections.Generic;
using System.Threading.Tasks;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IMedalService
    {
        Task<List<MedalDto>> GetAllAsync();
        Task<MedalDto> GetByIdAsync(int id);
        Task<MedalDto> CreateAsync(CreateMedalDto dto);
        Task<MedalDto> UpdateAsync(int id, UpdateMedalDto dto);
        Task DeleteAsync(int id);
    }
}
