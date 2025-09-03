using System;
using System.Threading.Tasks;
using VemboAPI.Domain.Entities;
using VemboAPI.Infrastructure.Data;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class UserManager : IUserManager
    {
        private readonly VemboDbContext _context;

        public UserManager(VemboDbContext context)
        {
            _context = context;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            var token = Guid.NewGuid().ToString();
            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<bool> ResetPasswordAsync(User user, string token, string newPassword)
        {
            user.PasswordHash = newPassword;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
