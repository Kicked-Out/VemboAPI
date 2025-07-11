using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserService
    {
        void CreateUser(string nickName, string password, string email);
        void UpdateUser(int id, string nickName, string password, string email);
        void DeleteUser(int id);

        UserDto GetUserById(int id);
        List<UserDto> GetAllUsers();
    }
}
