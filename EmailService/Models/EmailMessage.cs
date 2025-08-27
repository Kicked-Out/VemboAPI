namespace EmailService.Models
{
    public class EmailMessage
    {
        public IEnumerable<string> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public EmailMessage(IEnumerable<string> to, string subject, string content)
        {
            To = to;
            Subject = subject;
            Content = content;
        }
    }
}
