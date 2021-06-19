using System.Threading.Tasks;
using fooddelivery.Models;

namespace fooddelivery.Service.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(User user, string key);
        Task SendEmailRecoveryAsync(User user, string key);
    }
}