using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Constants;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = Policy.EmailVerified)]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        public OrdersController(IOrderService orderService, IAuthService authService)
        {
            _orderService = orderService;
            _authService = authService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(ulong id)
        {
            var result = await _orderService.GetByKeyAsync(id);
            if (result == null)
                return NotFound();

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
            var results = await _orderService.GetAllByUserIdAsync(userId, appview);
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
            var results = await _orderService.GetAllAsync(appview, null);
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            if (order == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _orderService.AddAsync(order);
            return Ok(order);
        }

        [HttpDelete]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Delete([FromQuery] ulong id)
        {
            var obj = await _orderService.GetByKeyAsync(id);
            if (obj == null)
                return NotFound("recurso não encontrado");


            await _orderService.DeleteAsync(obj);
            return Ok($"codigo {id} removido");
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Update(ulong id, [FromBody] Order order)
        {
            var obj = await _orderService.GetByKeyAsync(id);

            if (obj == null)
                return NotFound();

            if (order == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            await _orderService.UpdateAsync(order);
            return Ok(order);
        }
    }
}