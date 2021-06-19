using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using fooddelivery.Models;
using fooddelivery.Models.Constants;
using fooddelivery.Models.DTO;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IKeyService _keyService;

        public UsersController(IMapper mapper, IUserService userService, IKeyService keyService)
        {
            _mapper = mapper;
            _userService = userService;
            _keyService = keyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NoContent();
            var user = await _userService.GetUserByIdAsync(id);

            if (user != null)
                return Ok(user);
            else
                return NotFound("user not found");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var userList = await _userService.GetAllAsync(appview);
            return Ok(userList);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterUserDTO userDTO)
        {
            var user = _mapper.Map<RegisterUserDTO, User>(userDTO);

            var errors = await _userService.AddAsync(user, userDTO.Password);

            if (errors.Length != 0)
                return UnprocessableEntity(errors.ToString());
            else
                return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {

            var errors = await _userService.UpdateAsync(user);

            if (errors.Length != 0)
                return UnprocessableEntity(errors.ToString());
            else
                return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NoContent();

            var user = await _userService.GetUserByIdAsync(id);

            if (user != null)
            {

                var errors = await _userService.DeleteAsync(id);

                if (errors.Length != 0)
                    return UnprocessableEntity(errors.ToString());
                else
                    return Ok($"user id: {id} deleted");
            }
            else
            {
                return NotFound("user not found");
            }

        }

        [HttpGet("teste")]
        public async Task<IActionResult> Teste()
        {
            await _keyService.CreateNewKeyAsync(new User {Email ="uandersonlimacs@gmail.com"}, KeyType.Verification);

            return Ok("Tudo certinho");
        }
    }
}