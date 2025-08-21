using System.Threading.Tasks;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class EmailService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Here you would integrate with an actual email provider.
            // For now, this is just a placeholder implementation.
            return Task.CompletedTask;
        }
    }
}
