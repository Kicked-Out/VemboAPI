using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using System.Security.Claims;


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

        public void CreateUser(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.NickNameSlug = dto.NickName.ToLower().Replace(" ", "-");
            user.Level = 1;
            user.Rating = 0;
            user.IsPremium = false;
            user.XP = 0;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

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

        public UserDto GetUserById(string id)
        {
            var user = _dbContext.Users.Find(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            return _mapper.Map<UserDto>(user);
        }

        public UserDto GetUserByNickNameSlug(string nickNameSlug)
        {
            var user = _dbContext.Users
                .ToList()
                .Find(user => user.NickNameSlug == nickNameSlug);

            return _mapper.Map<UserDto>(user);
        }

        public void UpdateUser(int id, UpdateUserDto dto)
        {
            var user = _dbContext.Users.Find(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            _mapper.Map(dto, user);
            user.UpdatedAt = DateTime.UtcNow;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
    }
}
