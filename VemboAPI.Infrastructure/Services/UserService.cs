using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private VemboDbContext _vemboDbContext;
        public UserService(VemboDbContext vemboDbContext)
        {
            _vemboDbContext = vemboDbContext;
        }

        public void CreateUser(string nickName, string password, string email)
        {
            User user = new User
            {
                NickName = nickName,
                Password = password,
                Email = email,
                Level = 1,
                Rating = 0,
                IsPremium = false,
                XP = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _vemboDbContext.Users.Add(user);
            _vemboDbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User? user = _vemboDbContext.Users.Find(id);
            if (user != null)
            {
                _vemboDbContext.Users.Remove(user);
                _vemboDbContext.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
        }


        public List<User> GetAllUsers()
        {
            var collection = _vemboDbContext.Users.ToList();
            foreach (var user in collection)
            {
                user.NickName = user.NickName.ToUpper();   
            }
            return collection;
        }

        public User GetUserById(int id)
        {
            User? user = _vemboDbContext.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return user;
        }


        public void UpdateUser(int id, string nickName, string password, string email)
        {
            var user = _vemboDbContext.Users.Find(id);
            if (user != null)
            {
                user.NickName = nickName;
                user.Password = password;
                user.Email = email;
                user.UpdatedAt = DateTime.UtcNow;

                _vemboDbContext.Users.Update(user);
                _vemboDbContext.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
        }
    }
}
