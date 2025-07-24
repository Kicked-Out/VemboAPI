using VemboAPI.Domain.DTO;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserService
    {
        void CreateUser(CreateUserDto dto);
        void UpdateUser(int id, UpdateUserDto dto);

        void DeleteUser(int id);

        UserDto GetUserById(int id);
        List<UserDto> GetAllUsers();
    }
}
