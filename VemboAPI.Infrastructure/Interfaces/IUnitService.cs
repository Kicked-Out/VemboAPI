using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUnitService
    {
        List<UnitDto> GetAllUnits();
        List<UnitDto> GetAllUnitsByTopicId(int topicId);
        UnitDto GetUnitById(int id);
        UnitDto CreateUnit(CreateUnitDto dto);
        void UpdateUnit(int id, UpdateUnitDto dto);
        void DeleteUnit(int id);
    }
}
