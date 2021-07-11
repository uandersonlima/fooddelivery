using System.Threading.Tasks;
using fooddelivery.Libraries.Template;
using fooddelivery.Models.Constants;
using fooddelivery.Models.Helpers;
using fooddelivery.Models.Users;
using fooddelivery.Service.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace fooddelivery.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings appsettings;
        private readonly IHttpContextAccessor accessor;

        public EmailService(IOptions<EmailSettings> appsettings, IHttpContextAccessor accessor)
        {
            this.appsettings = appsettings.Value;
            this.accessor = accessor;
        }
        public async Task SendEmailRecoveryAsync(User user, string key)
        {

            var email = new MimeMessage();
            string text = EmailTemplate.EmailPage(
                nome: user.Name,
                key: key,
                type: KeyType.Recovery,
                contactLink: null
            );

            email.From.Add(MailboxAddress.Parse(appsettings.SmtpUser));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Sushi Delivery - TecnoSystem - Email de Recuperação - " + user.Name;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = text
            };

            // Send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(appsettings.SmtpHost, appsettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(appsettings.SmtpUser, appsettings.SmtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        public async Task SendEmailVerificationAsync(User user, string key)
        {
            var email = new MimeMessage();
            string text = EmailTemplate.EmailPage(
                nome: user.Name,
                key: key,
                type: KeyType.Verification,
                contactLink: null
            );

            email.From.Add(MailboxAddress.Parse(appsettings.SmtpUser));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Sushi Delivery - TecnoSystem - Email de verificação - " + user.Name;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = text
            };

            // Send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(appsettings.SmtpHost, appsettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(appsettings.SmtpUser, appsettings.SmtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}