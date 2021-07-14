using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using fooddelivery.Models.DTO;
using fooddelivery.Models.Helpers;
using fooddelivery.Models.Users;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace fooddelivery.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAddressService _addressService;
        private readonly JWTSettings _jwtSettings;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;


        public AuthService(IAddressService addressService, IOptions<JWTSettings> jwtSettings, SignInManager<User> signInManager, RoleManager<Role> roleManager,
        UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _addressService = addressService;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<TokenDTO> CreateTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _jwtSettings.SecretKey;
            var encryptedSecretKey = Encoding.ASCII.GetBytes(secretKey);

            var claims = await _userManager.GetClaimsAsync(user);
            var roles = new List<string>(await _userManager.GetRolesAsync(user));
            roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));


            var exp = DateTime.UtcNow.AddHours(2);
            var sign = new SigningCredentials(new SymmetricSecurityKey(encryptedSecretKey), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = exp,
                SigningCredentials = sign
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenDTO = new TokenDTO
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = exp,
                RefreshToken = Guid.NewGuid().ToString("N"),
                ExpirationRefreshToken = DateTime.UtcNow.AddHours(4)
            };

            return tokenDTO;
        }

        public async Task<User> GetLoggedUserAsync()
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            if (user != null)
            {
                var addresses = await _addressService.GetAllByUserIdAsync(user.Id, new AppView());
                user.Addresses = addresses;
            }
            return user;
        }
    }
}