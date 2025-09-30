using VemboAPI.Domain.DTO;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserLeaderBoardService
    {
        Task<List<UserLeaderBoardEntryDto>> GetAllAsync();
        Task<UserLeaderBoardEntryDto> GetByIdAsync(int id);
        Task<UserLeaderBoardEntryDto> CreateAsync(CreateUserLeaderBoardEntryDto dto);
        Task UpdateAsync(int id, UpdateUserLeaderBoardEntryDto dto);
        Task UpdateTotalXPAsync(string userId, UpdateUserTotalXPDto dto);
        Task DeleteAsync(int id);
    }

}

