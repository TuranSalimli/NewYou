using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using NewYou.Application.Abstraction.Services;

namespace NewYou.Persistence.Concretes.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var smtpSettings = _configuration.GetSection("SmtpSettings");

        using var client = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"]!))
        {
            Credentials = new NetworkCredential(smtpSettings["Email"], smtpSettings["Password"]),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpSettings["Email"]!),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(to);

        await client.SendMailAsync(mailMessage);
    }
}