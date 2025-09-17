using VemboAPI.Domain.DTOs;
namespace VemboAPI.Infrastructure.Interfaces
{
	public interface ILevelService
	{
		Task<List<LevelDto>> GetAllLevels();
		Task<List<LevelDto>> GetAllLevelsByUnitId(int unitId);
		Task<LevelDto> GetLevelById(int id);
        Task<LevelDto> CreateLevel(CreateLevelDto dto);
        Task UpdateLevel(int id, UpdateLevelDto dto);
        Task DeleteLevel(int id);
	}
}

