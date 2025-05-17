using System.Net.Mail;
using System.Net;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Example using SMTP (can be replaced with SendGrid or another service)
        var smtpClient = new SmtpClient
        {
            Host = _config["Email:Smtp:Host"],
            Port = int.Parse(_config["Email:Smtp:Port"]),
            Credentials = new NetworkCredential(
                _config["Email:Smtp:Username"],
                _config["Email:Smtp:Password"]),
            EnableSsl = true
        };

        var message = new MailMessage
        {
            From = new MailAddress(_config["Email:Smtp:From"]),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        message.To.Add(email);

        await smtpClient.SendMailAsync(message);
    }
}
