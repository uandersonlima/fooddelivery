using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
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
            email.From.Add(MailboxAddress.Parse(appsettings.SmtpUser));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Sushi Delivery - TecnoSystem - Email de Recuperação - " + user.Name;
            email.Body = new TextPart(TextFormat.Html) { Text = "Seu código de recuperação é " + key + " <b> Se você não fez essa solicitação ignore essa mensagem"};

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
            email.From.Add(MailboxAddress.Parse(appsettings.SmtpUser));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Sushi Delivery - TecnoSystem - Email de verificação - " + user.Name;
            email.Body = new TextPart(TextFormat.Html) { Text = "Seu código de ativacao é " + key + " <b> Se você não fez essa solicitação ignore essa mensagem"};

            // Send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(appsettings.SmtpHost, appsettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(appsettings.SmtpUser, appsettings.SmtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}