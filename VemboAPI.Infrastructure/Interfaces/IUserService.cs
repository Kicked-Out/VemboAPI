using VemboAPI.Domain.DTOs;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(CreateUserDto dto);
        Task UpdateUser(string id, UpdateUserDto dto);
        Task UpdateRoleAsync(string userId, string newRole);

        Task DeleteUser(int id);

        Task<UserDto> GetUserByNickNameSlug(string nickNameSlug);
        Task<UserDto> GetUserById(string id);
        Task<List<UserDto>> GetAllUsers();
    }
}
