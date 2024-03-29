using System.Threading.Tasks;
using fooddelivery.Models.Constants;
using fooddelivery.Models.Contracts;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using fooddelivery.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace fooddelivery.Controllers.Api
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = Policy.EmailVerified)]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedService;
        private readonly IAuthService _authService;

        public FeedbacksController(IFeedbackService feedService, IAuthService authService)
        {
            _feedService = feedService;
            _authService = authService;
        }

        [HttpGet("{userId}/{orderId}")]
        public async Task<IActionResult> Get(ulong userId, ulong orderId)
        {
            var result = await _feedService.GetByKeyAsync(userId, orderId);
            if (result == null)
            {
                return NotFound();
            }

            var isAdmin = HttpContext.User.IsInRole(Policy.Admin);
            var loggedInUser = await _authService.GetLoggedUserAsync();

            if (!isAdmin && result.UserId != loggedInUser.Id)
            {
                return Unauthorized("Você não tem permissão para ver esse endereço");
            }

            return Ok(result);
        }

        [HttpGet("AllByUserId/{userId}")]
        public async Task<IActionResult> GetAll(ulong userId, [FromQuery] AppView appview)
        {
            var results = await _feedService.GetAllByUserIdAsync(userId, appview);
            if (results.Count > 0)
            {
                var isAdmin = HttpContext.User.IsInRole(Policy.Admin);
                var loggedInUser = await _authService.GetLoggedUserAsync();
                if (!isAdmin && results[0].UserId != loggedInUser.Id)
                {
                    return Unauthorized("Você não tem permissão para ver esse feedback");
                }
            }
            return Ok(results);
        }

        [HttpGet]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var results = await _feedService.GetAllAsync(appview, x => x.Note.Contains(appview.Search));
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromServices] IUserService userService, [FromServices] IOptions<EmailSettings> emailsettings, [FromServices] IHubContext<NotificationsHubService, INotificationsHubService> notificationHub, [FromBody] Feedbacks feedbacks)
        {
            if (feedbacks == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _feedService.AddAsync(feedbacks);


            var user = await userService.GetUserByEmailAsync(emailsettings.Value.SmtpUser);
            await notificationHub.Clients.User(user.Id.ToString()).ReportFeedbacksUpdatesAsync("Novo feedback!");

            return Ok(feedbacks);
        }

        [HttpDelete("{userId}/{orderId}")]
        public async Task<IActionResult> Delete(ulong userId, ulong orderId)
        {

            var result = await _feedService.GetByKeyAsync(userId, orderId);
            if (result is null)
                return NotFound("recurso não encontrado");

            await _feedService.DeleteAsync(result);
            return Ok($"Feedback do pedido {orderId} do usuário {userId} removido");
        }

        [HttpPut("{userId}/{orderId}")]
        public async Task<IActionResult> Update(ulong userId, ulong orderId, [FromBody] Feedbacks feedbacks)
        {
            var obj = await _feedService.GetByKeyAsync(userId, orderId);

            if (obj is null)
                return NotFound();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            obj.Note = feedbacks.Note;
            obj.Score = feedbacks.Score;

            await _feedService.UpdateAsync(obj);
            return Ok(obj);
        }
    }
}