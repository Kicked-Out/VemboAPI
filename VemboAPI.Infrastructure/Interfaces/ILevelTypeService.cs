using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface ILevelTypeService
    {
        Task<List<LevelTypeDto>> GetAll();
        Task<LevelTypeDto> GetById(int id);
        Task<LevelTypeDto> Create(CreateLevelTypeDto dto);
        Task Update(int id, UpdateLevelTypeDto dto);
        Task Delete(int id);
    }
}
