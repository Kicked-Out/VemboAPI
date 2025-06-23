using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUnitService
    {
        List<UnitDto> GetAllUnits();
        UnitDto GetUnitById(int id);
        UnitDto CreateUnit(string title, string description, int order, int topicId);
        void UpdateUnit(int id, string title, string description, int order, int topicId);
        void DeleteUnit(int id);
    }
}
