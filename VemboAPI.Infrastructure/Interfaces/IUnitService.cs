using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUnitService
    {
        Task<List<UnitDto>> GetAllUnits();
        Task<List<UnitDto>> GetAllUnitsByTopicId(int topicId);
        Task<UnitDto> GetUnitById(int id);
        Task<UnitDto> CreateUnit(CreateUnitDto dto);
        Task UpdateUnit(int id, UpdateUnitDto dto);
        Task DeleteUnit(int id);
    }
}
