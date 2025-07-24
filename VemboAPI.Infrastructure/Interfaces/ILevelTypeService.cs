using VemboAPI.Domain.DTOs;
using System.Collections.Generic;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface ILevelTypeService
    {
        List<LevelTypeDto> GetAll();
        LevelTypeDto GetById(int id);
        LevelTypeDto Create(CreateLevelTypeDto dto);
        void Update(int id, UpdateLevelTypeDto dto);
        void Delete(int id);
    }
}
