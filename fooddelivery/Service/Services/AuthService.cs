using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace fooddelivery.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly JWTSettings jwtSettings;
        private readonly SignInManager<User> signInManager;
        private readonly IHttpContextAccessor contextAccessor;

        public AuthService(IOptions<JWTSettings> jwtSettings, SignInManager<User> signInManager, IHttpContextAccessor contextAccessor)
        {
            this.jwtSettings = jwtSettings.Value;
            this.signInManager = signInManager;
            this.contextAccessor = contextAccessor;
        }

        public async Task<string> CreateTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var x = jwtSettings.SecretKey;
            var key = Encoding.ASCII.GetBytes(x);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> GetLoggedUserAsync()
        {
            return await signInManager.UserManager.GetUserAsync(contextAccessor.HttpContext.User);
        }
    }
}