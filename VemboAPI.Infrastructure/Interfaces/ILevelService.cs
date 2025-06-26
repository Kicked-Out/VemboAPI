using System.Collections.Generic;
using VemboAPI.Domain.DTOs;
namespace VemboAPI.Infrastructure.Interfaces
{
	public interface ILevelService
	{
		List<LevelDto> GetAllLevels();
		LevelDto GetLevelById(int id);
		LevelDto CreateLevel(string title, int unitId, int order);
		void UpdateLevel(int id, string title, int unitId, int order);
		void DeleteLevel(int id);
	}
}

