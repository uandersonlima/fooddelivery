using System.Threading.Tasks;
using fooddelivery.Models.Constants;
using fooddelivery.Models.Contracts;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(ulong id)
        {
            var result = await _feedService.GetByKeyAsync(id);
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
                    return Unauthorized("Você não tem permissão para ver esse endereço");
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
        public async Task<IActionResult> Create([FromBody] Feedbacks feedbacks)
        {
            if (feedbacks == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _feedService.AddAsync(feedbacks);
            return Ok(feedbacks);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] ulong id)
        {

            var obj = await _feedService.GetByKeyAsync(id);
            if (obj == null)
                return NotFound("recurso não encontrado");

            await _feedService.DeleteAsync(obj);
            return Ok($"codigo {id} removido");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ulong id, [FromBody] Feedbacks feedbacks)
        {
            var obj = await _feedService.GetByKeyAsync(id);

            if (obj == null)
                return NotFound();

            if (feedbacks == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _feedService.UpdateAsync(feedbacks);
            return Ok(feedbacks);
        }
    }
}