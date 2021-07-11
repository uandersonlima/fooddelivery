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
    public class SubordersController : ControllerBase
    {
        private readonly ISuborderService _suborderService;

        public SubordersController(ISuborderService suborderService)
        {
            _suborderService = suborderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(ulong id)
        {
            var result = await _suborderService.GetByKeyAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var results = await _suborderService.GetAllAsync(appview, null);
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Suborder suborder)
        {
            if (suborder == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _suborderService.AddAsync(suborder);
            return Ok(suborder);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] ulong id)
        {
            var obj = await _suborderService.GetByKeyAsync(id);
            if (obj == null)
                return NotFound("recurso não encontrado");

            await _suborderService.DeleteAsync(obj);
            return Ok($"codigo {id} removido");
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ulong id, [FromBody] Suborder suborder)
        {
            var obj = await _suborderService.GetByKeyAsync(id);

            if (obj == null)
                return NotFound();

            if (suborder == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            await _suborderService.UpdateAsync(suborder);
            return Ok(suborder);
        }
    }
}