using System;
using System.Threading.Tasks;
using fooddelivery.Libraries.Template;
using fooddelivery.Models;
using fooddelivery.Models.Constants;
using fooddelivery.Models.DTO;
using fooddelivery.Models.Users;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAddressService _addressService;
        private readonly ITokenJWTService _tokenJWTService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IAddressService addressService, ITokenJWTService tokenJWTService, IUserService userService)
        {
            _authService = authService;
            _addressService = addressService;
            _tokenJWTService = tokenJWTService;
            _userService = userService;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginUserDTO login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserByEmailAsync(login.Email);

                if (user != null)
                {
                    if (await _userService.CheckPasswordAsync(user, login.Password))
                    {
                        return await GenerateToken(user);
                    }

                }
                return NotFound("wrong username or password");
            }
            return UnprocessableEntity(ModelState);
        }

        [HttpPost("Renew")]
        public async Task<IActionResult> Renew([FromBody] TokenDTO tokenDTO)
        {
            var refreshTokenDB = await _tokenJWTService.GetTokenByRefreshTokenAsync(tokenDTO.RefreshToken);

            if (refreshTokenDB == null)
                return NotFound("refreshtoken already used or not exist");


            refreshTokenDB.Updated = DateTime.Now;
            refreshTokenDB.Used = true;
            await _tokenJWTService.UpdateAsync(refreshTokenDB);


            var user = await _userService.GetUserByIdAsync(refreshTokenDB.UserId);

            return await GenerateToken(user);
        }


        [HttpGet, Authorize]
        public async Task<IActionResult> GetLoggedUser()
        {
            return Ok(await _authService.GetLoggedUserAsync());
        }

        private async Task<IActionResult> GenerateToken(User user)
        {
            var token = await _authService.CreateTokenAsync(user);
            var tokenModel = new TokenJWT()
            {
                RefreshToken = token.RefreshToken,
                ExpirationToken = token.Expiration,
                ExpirationRefreshToken = token.ExpirationRefreshToken,
                UserId = user.Id,
                Created = DateTime.Now,
                Used = false
            };
            await _tokenJWTService.AddAsync(tokenModel);

            return Ok(token);
        }

    }

}