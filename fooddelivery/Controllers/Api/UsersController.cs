using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using fooddelivery.Models;
using fooddelivery.Models.DTO;
using fooddelivery.Models.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UsersController(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NoContent();
            var user = await _userManager.FindByIdAsync(id);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]AppView appview)
        {
            var userList =  _userManager.Users.AsNoTracking();
            if(appview.CheckSearch())
            {
               userList = userList.Where(u => u.UserName.Contains(appview.Search));
            }
            return Ok(await userList.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterUserDTO userDTO)
        {
            var user = _mapper.Map<RegisterUserDTO, User>(userDTO);

            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
            {
                return Ok(user);
            }
            else
            {
                var str = new StringBuilder();
                foreach (var erro in result.Errors)
                {
                    str.Append(erro.Code + ":\n" + erro.Description);
                }
                return UnprocessableEntity(str.ToString());
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(user);
            }
            else
            {
                var str = new StringBuilder();
                foreach (var erro in result.Errors)
                {
                    str.Append(erro.Code + ":\n" + erro.Description);
                }
                return UnprocessableEntity(str.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NoContent();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                NotFound("Usuário não existe");

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(user + "Usuário deletado");
            }
            else
            {
                var str = new StringBuilder();
                foreach (var erro in result.Errors)
                {
                    str.Append(erro.Code + ":\n" + erro.Description);
                }
                return UnprocessableEntity(str.ToString());
            }
        }
    }
}