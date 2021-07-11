using System.Threading.Tasks;
using fooddelivery.Models.DTO;
using fooddelivery.Models.Users;

namespace fooddelivery.Service.Interfaces
{
    public interface IAuthService
    {
        Task<User> GetLoggedUserAsync();
        Task<TokenDTO> CreateTokenAsync(User user);
    }
    
}