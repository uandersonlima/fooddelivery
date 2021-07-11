using System.Collections.Generic;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class TokenJWTService : ITokenJWTService
    {
        private readonly ITokenJWTRepository _tokenJWTRepository;

        public TokenJWTService(ITokenJWTRepository tokenJWTRepository)
        {
            _tokenJWTRepository = tokenJWTRepository;
        }

        public async Task AddAsync(TokenJWT tokenJWT)
        {
            await _tokenJWTRepository.AddAsync(tokenJWT);
        }

        public async Task DeleteAsync(TokenJWT tokenJWT)
        {
            await _tokenJWTRepository.DeleteAsync(tokenJWT);
        }

        public async Task DeleteMultiplesAsync(List<TokenJWT> tokensJWT)
        {
            await _tokenJWTRepository.DeleteMultiplesAsync(tokensJWT);
        }

        public async Task<List<TokenJWT>> GetAllTokensUsedOrExpiredAsync()
        {
            return await _tokenJWTRepository.GetAllTokensUsedOrExpiredAsync();
        }

        public async Task<TokenJWT> GetTokenByRefreshTokenAsync(string refreshToken)
        {
            return await _tokenJWTRepository.GetTokenByRefreshTokenAsync(refreshToken);
        }

        public async Task UpdateAsync(TokenJWT tokenJWT)
        {
            await _tokenJWTRepository.UpdateAsync(tokenJWT);
        }
    }
}