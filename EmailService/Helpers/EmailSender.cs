using EmailService.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace EmailService.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            MimeMessage emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse(_config["EmailSettings:SenderEmail"]));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(TextFormat.Html) { Text = body };

            using (SmtpClient smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:SmtpPort"]), MailKit.Security.SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_config["EmailSettings:SenderEmail"], _config["EmailSettings:SenderPassword"]);
                await smtpClient.SendAsync(emailToSend);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
