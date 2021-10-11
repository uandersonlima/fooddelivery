using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using fooddelivery.Models.Access;
using fooddelivery.Models.Constants;
using fooddelivery.Models.DTO;
using fooddelivery.Models.Helpers;
using fooddelivery.Models.Users;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = Policy.EmailVerified)]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IKeyService _keyService;
        private readonly IPermissionsService _permissionsService;
        private readonly UserManager<User> _userManager;

        public UsersController(IAuthService authService, IMapper mapper, IUserService userService, IKeyService keyService, IPermissionsService permissionsService, UserManager<User> userManager)
        {
            _authService = authService;
            _mapper = mapper;
            _userService = userService;
            _keyService = keyService;
            _permissionsService = permissionsService;
            _userManager = userManager;
        }


        /// <summary>
        /// é necessário está logado
        /// </summary>
        /// <param name=""></param>'
        /// <returns>é necessário está logado</returns>

        [HttpPost("newEmailConfirmation"), AllowAnonymous]
        public async Task<IActionResult> NewEmailConfirmation()
        {
            var loggedInUser = await _authService.GetLoggedUserAsync();

            if (loggedInUser is null)
                return BadRequest();

            if (loggedInUser.EmailConfirmed)
                return UnprocessableEntity("O email já foi confirmado");

            var serverkey = new AccessKey { Email = loggedInUser.Email, KeyType = KeyType.Verification };
            var elapsedTime = await _keyService.ElapsedTimeAsync(serverkey);
            var _15min = new TimeSpan(0, 15, 0);

            if (elapsedTime < _15min)
            {
                var msg = string.Format("chave não gerada, aguarde {0} min antes de pedir uma novo chave de confirmação!",
                        _15min.Subtract(elapsedTime).ToString(@"mm\:ss"));
                return BadRequest(msg);
            }
            await _keyService.CreateNewKeyAsync(loggedInUser, KeyType.Verification);

            return Ok();
        }

        [HttpPost("confirmEmail"), AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ClientKeyDTO clientKeyDto)
        {
            //A partir do teu authorization recupera o usuário logado.
            var loggedInUser = await _authService.GetLoggedUserAsync();
            if (loggedInUser is null)
                return Unauthorized("é preciso estar logado para confirmar o email");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (string.IsNullOrEmpty(clientKeyDto.ClientKey))
                return BadRequest("forneça os dados esperados");

            var serverKey = await _keyService.SearchKeyAsync(loggedInUser.Email, clientKeyDto.ClientKey, KeyType.Verification);

            if (serverKey is null)
                return NotFound("chave correspondente ao email não encontrada");

            var user = await _userService.GetUserByEmailAsync(loggedInUser.Email);

            if (user is null)
                return NotFound("não pudemos processar essa chamada, nenhum usuário com email correspondente encontrado");

            if (!user.EmailConfirmed)
            {
                user.EmailConfirmed = !user.EmailConfirmed;
                await _userService.UpdateAsync(user);
                await _keyService.DeleteAsync(serverKey);
                await _userManager.RemoveClaimAsync(user, new Claim(Policy.EmailVerified, false.ToString()));
                await _userManager.AddClaimAsync(user, new Claim(Policy.EmailVerified, true.ToString()));
                return Ok("Email confirmado com sucesso");
            }
            return Ok("Email já foi confirmado");
        }

        [HttpPost("newEmailRecovery"), AllowAnonymous]
        public async Task<IActionResult> newEmailRecovery([FromBody] EmailDTO email)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var serverUser = await _userService.GetUserByEmailAsync(email.EmailAddress);
            if (serverUser == null)
                return NotFound("user not found");

            var serverkey = new AccessKey { Email = email.EmailAddress, KeyType = KeyType.Recovery };
            var elapsedTime = await _keyService.ElapsedTimeAsync(serverkey);
            var _15min = new TimeSpan(0, 15, 0);

            if (elapsedTime < _15min)
            {
                var msg = string.Format("chave não gerada, aguarde {0} min antes de pedir uma novo chave de confirmação!",
                        _15min.Subtract(elapsedTime).ToString(@"mm\:ss"));
                return BadRequest(msg);
            }
            await _keyService.CreateNewKeyAsync(serverUser, KeyType.Recovery);

            return Ok();
        }


        [HttpPost("ResetPassword"), AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordDTO userResetPassword)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var serverKey = await _keyService.SearchKeyAsync(userResetPassword.EmailAddress, userResetPassword.ClientKey, KeyType.Recovery);

            if (serverKey == null)
                return NotFound("chave correspondente ao email não encontrada");

            var user = await _userService.GetUserByEmailAsync(userResetPassword.EmailAddress);

            if (user == null)
                return NotFound("não pudemos processar essa chamada, nenhum usuário com email correspondente encontrado");

            var tokenResetPassword = await _userService.GeneratePasswordResetTokenAsync(user);
            var errors = await _userService.ResetPasswordAsync(user, tokenResetPassword, userResetPassword.Password);

            if (errors.Length != 0)
                return UnprocessableEntity(errors.ToString());
            else
            { 
                await _keyService.DeleteAsync(serverKey);
                return Ok("Password updated");
            }
                
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            var loggedInUser = await _authService.GetLoggedUserAsync();
            if (loggedInUser == null)
                return BadRequest("Algo deu errado");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var errors = await _userService.ChangePasswordAsync(loggedInUser, changePassword.CurrentPassword, changePassword.NewPassword);
            if (errors.Length != 0)
                return UnprocessableEntity(errors.ToString());

            return Ok("Password updated");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(ulong id)
        {
            if (id == 0)
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

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] RegisterUserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var user = _mapper.Map<RegisterUserDTO, User>(userDTO);

            var errors = await _userService.AddAsync(user, userDTO.Password);

            if (errors.Length != 0)
                return UnprocessableEntity(errors.ToString());

            await _keyService.CreateNewKeyAsync(user, KeyType.Verification);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserProfile ClientUser)
        {
            var ServerUser = await _authService.GetLoggedUserAsync();
            if (ServerUser.Id != ClientUser.Id)
                return BadRequest("Usuário não pode atualizar esse perfil");

            var DbUser = await _userService.GetUserByIdAsync(ClientUser.Id);

            DbUser.Name = ClientUser.Name;
            DbUser.PhoneNumber = ClientUser.PhoneNumber;

            var errors = await _userService.UpdateAsync(DbUser);

            if (errors.Length != 0)
                return UnprocessableEntity(errors.ToString());
            else
                return Ok(DbUser);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ulong id)
        {
            if (id == 0)
                return NoContent();

            var user = await _userService.GetUserByIdAsync(id);

            if (user != null)
            {

                var errors = await _userService.DeleteAsync(user);

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

    }
}