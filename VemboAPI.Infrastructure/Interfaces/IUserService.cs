using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserService
    {
        void CreateUser(CreateUserDto dto);
        void UpdateUser(int id, UpdateUserDto dto);
        void DeleteUser(int id);

        UserDto GetUserByNickNameSlug(string nickNameSlug);
        UserDto GetUserById(string id);
        List<UserDto> GetAllUsers();
    }
}
