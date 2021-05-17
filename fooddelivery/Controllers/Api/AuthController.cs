using System.Threading.Tasks;
using fooddelivery.Models.DTO;
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
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
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
                        var token = await _authService.CreateTokenAsync(user);
                        return Ok(token);
                    }

                }
                return NotFound("wrong username or password");
            }
            return UnprocessableEntity(ModelState);
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetLoggedUser()
        {
            return Ok(await _authService.GetLoggedUserAsync());
        }
    }

}