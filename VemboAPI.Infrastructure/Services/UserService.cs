using System;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;
using VemboAPI.Domain.Entities;
using VemboAPI.Domain.DTOs;
using AutoMapper;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace VemboAPI.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly VemboDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public UserService(VemboDbContext dbContext, IMapper mapper, IUploadService uploadService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task CreateUser(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            
            user.NickNameSlug = dto.NickName.ToLower().Replace(" ", "-");
            user.Level = 1;
            user.Rating = 0;
            user.IsPremium = false;
            user.XP = 0;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }


        public async Task DeleteUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _dbContext.Users.ToListAsync();

            // якщо ти хочеш залишити NickName в UpperCase:
            foreach (var u in users)
                u.NickName = u.NickName.ToUpper();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByNickNameSlug(string nickNameSlug)
        {
            var user = await _dbContext.Users
                .Where(user => user.NickNameSlug == nickNameSlug)
                .ToListAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUser(string id, UpdateUserDto dto)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            _mapper.Map(dto, user);
            user.NickNameSlug = dto.NickName.ToLower().Replace(" ", "-");
            user.UpdatedAt = DateTime.UtcNow;

            if (dto.Avatar != null)
            {
                string path = _uploadService.UploadFile("Profiles", id, dto.Avatar);

                user.AvatarUrl = path;
            }

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateRoleAsync(string userId, string newRole)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            user.Role = newRole;
            user.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }

        public async Task BlockUserAsync(string userId, DateTimeOffset? lockedUntil, string? reason = null)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            user.LockoutEnabled = true;
            user.LockoutEnd = lockedUntil ?? DateTimeOffset.MaxValue;
            user.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UnblockUserAsync(string userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            user.LockoutEnd = null;
            user.LockoutEnabled = false;
            user.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }

    }
}
