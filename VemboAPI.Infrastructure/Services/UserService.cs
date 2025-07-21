using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;

namespace VemboAPI.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(VemboDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void CreateUser(string nickName, string password, string email)
        {
            var user = new User
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

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public List<UserDto> GetAllUsers()
        {
            var users = _dbContext.Users.ToList();

            // якщо ти хочеш залишити NickName в UpperCase:
            foreach (var u in users)
                u.NickName = u.NickName.ToUpper();

            return _mapper.Map<List<UserDto>>(users);
        }

        public UserDto GetUserById(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            user.NickName = user.NickName.ToUpper();

            return _mapper.Map<UserDto>(user);
        }

        public void UpdateUser(int id, string nickName, string password, string email)
        {
            var user = _dbContext.Users.Find(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            user.NickName = nickName;
            user.Password = password;
            user.Email = email;
            user.UpdatedAt = DateTime.UtcNow;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
    }
}
