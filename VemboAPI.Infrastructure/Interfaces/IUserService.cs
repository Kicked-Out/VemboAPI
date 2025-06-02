using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserService
    {
        public void CreateUser(string nickName, string password, string email);

        public void UpdateUser(int id, string nickName, string password, string email);

        public void DeleteUser(int id);

        public User GetUserById(int id);

        public List<User> GetAllUsers();

    }
}
