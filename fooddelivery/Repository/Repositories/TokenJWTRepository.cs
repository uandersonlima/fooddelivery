using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Repository.Repositories
{
    public class TokenJWTRepository : ITokenJWTRepository
    {
        private readonly FoodDeliveryContext _context;

        public TokenJWTRepository(FoodDeliveryContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task AddAsync(TokenJWT tokenJWT)
        {
            await _context.TokensJWT.AddAsync(tokenJWT);
            await _context.SaveChangesAsync();
        }
        public async Task<TokenJWT> GetTokenByRefreshTokenAsync(string refreshToken)
        {
            return await _context.TokensJWT.FirstOrDefaultAsync(tok => tok.RefreshToken == refreshToken && tok.Used == false);
        }

        public async Task<List<TokenJWT>> GetAllTokensUsedOrExpiredAsync()
        {
            return await _context.TokensJWT.Where(tok => tok.ExpirationRefreshToken < DateTime.UtcNow || tok.Used == true).ToListAsync();
        }

        public async Task DeleteMultiplesAsync(List<TokenJWT> tokensJWT)
        {
            _context.TokensJWT.RemoveRange(tokensJWT);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TokenJWT tokenJWT)
        {
            _context.TokensJWT.Remove(tokenJWT);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TokenJWT tokenJWT)
        {
            _context.TokensJWT.Update(tokenJWT);
            await _context.SaveChangesAsync();
        }
    }
}