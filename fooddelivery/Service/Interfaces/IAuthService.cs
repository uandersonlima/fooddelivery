using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Users;

namespace fooddelivery.Service.Interfaces
{
    public interface IAuthService
    {
        Task<User> GetLoggedUserAsync();
        Task<string> CreateTokenAsync(User user);
    }
    
}