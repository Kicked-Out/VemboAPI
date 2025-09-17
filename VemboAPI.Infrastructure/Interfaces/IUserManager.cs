using System.Threading.Tasks;
using VemboAPI.Domain.Entities;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IUserManager
    {
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<bool> ResetPasswordAsync(User user, string token, string newPassword);
    }
}
