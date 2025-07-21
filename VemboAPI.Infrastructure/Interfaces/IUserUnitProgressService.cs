using System.Collections.Generic;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserUnitProgressService
    {
        List<UserUnitProgressDto> GetAllUserUnitProgress();
        UserUnitProgressDto GetUserUnitProgressById(int id);
        UserUnitProgressDto CreateUserUnitProgress(CreateUserUnitProgressDto dto);
        void UpdateUserUnitProgress(int id, UpdateUserUnitProgressDto dto);

        void DeleteUserUnitProgress(int id);
    }
}
