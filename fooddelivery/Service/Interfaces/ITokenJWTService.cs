using System.Collections.Generic;
using System.Threading.Tasks;
using fooddelivery.Models;

namespace fooddelivery.Service.Interfaces
{
    public interface ITokenJWTService
    {
        Task AddAsync(TokenJWT tokenJWT);
        Task<TokenJWT> GetTokenByRefreshTokenAsync(string refreshToken);
        Task<List<TokenJWT>> GetAllTokensUsedOrExpiredAsync();
        Task DeleteMultiplesAsync(List<TokenJWT> tokensJWT);
        Task DeleteAsync(TokenJWT tokenJWT);
        Task UpdateAsync(TokenJWT tokenJWT);
    }
}