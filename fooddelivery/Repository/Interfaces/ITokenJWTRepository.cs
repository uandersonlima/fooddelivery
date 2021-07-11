using System.Collections.Generic;
using System.Threading.Tasks;
using fooddelivery.Models;

namespace fooddelivery.Repository.Interfaces
{
    public interface ITokenJWTRepository
    {
        Task AddAsync(TokenJWT tokenJWT);
        Task<TokenJWT> GetTokenByRefreshTokenAsync(string refreshToken);
        Task<List<TokenJWT>> GetAllTokensUsedOrExpiredAsync();
        Task DeleteMultiplesAsync(List<TokenJWT> tokensJWT);
        Task DeleteAsync(TokenJWT tokenJWT);
        Task UpdateAsync(TokenJWT tokenJWT);
    }
}