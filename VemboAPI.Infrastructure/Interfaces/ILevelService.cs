using System.Collections.Generic;
using VemboAPI.Domain.DTOs;
namespace VemboAPI.Infrastructure.Interfaces
{
	public interface ILevelService
	{
		List<LevelDto> GetAllLevels();
		List<LevelDto> GetAllLevelsByUnitId(int unitId);
		LevelDto GetLevelById(int id);
        LevelDto CreateLevel(CreateLevelDto dto);
        void UpdateLevel(int id, UpdateLevelDto dto);
        void DeleteLevel(int id);
	}
}

