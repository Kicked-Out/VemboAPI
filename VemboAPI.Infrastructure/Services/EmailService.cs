using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (email == null)
            {
                return;
            }
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(MailboxAddress.Parse(_settings.From));
            mailMessage.To.Add(MailboxAddress.Parse(email));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.Host, _settings.Port, _settings.UseSsl);
            await client.AuthenticateAsync(_settings.Username, _settings.Password);
            await client.SendAsync(mailMessage);
            await client.DisconnectAsync(true);
        }
    }

    public class EmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseSsl { get; set; } = true;
        public string From { get; set; }
    }
}
