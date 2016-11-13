using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.Extensions.Options;

namespace WebApp.API.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender
    {
        private readonly EmailSettings _settings;

        public AuthMessageSender(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_settings.FromFullname, _settings.FromEmail));
            emailMessage.To.Add(new MailboxAddress(email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_settings.SmtpServer, 587, SecureSocketOptions.Auto);
                await client.AuthenticateAsync(_settings.SendGridUsername, _settings.SendGridPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
