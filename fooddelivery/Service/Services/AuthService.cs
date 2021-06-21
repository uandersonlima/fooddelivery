using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using fooddelivery.Models;
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

        public async Task<string> CreateTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var x = _jwtSettings.SecretKey;
            var key = Encoding.ASCII.GetBytes(x);

            var claims = await _userManager.GetClaimsAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));

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
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            var addresses = await _addressService.GetAllByUserIdAsync(user.Id, new AppView());
            user.Addresses = addresses;
            return user;
        }
    }
}