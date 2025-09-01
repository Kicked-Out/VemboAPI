using System.Collections.Generic;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserQuestService
    {
        List<UserQuestDto> GetAll();
        UserQuestDto GetById(int id);
        UserQuestDto Create(CreateUserQuestDto dto);
        void Update(int id, UpdateUserQuestDto dto);
        void Delete(int id);
    }
}
