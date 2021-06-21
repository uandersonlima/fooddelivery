using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Users;

namespace fooddelivery.Service.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(User user, string key);
        Task SendEmailRecoveryAsync(User user, string key);
    }
}