using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
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
            var obj = _suborderService.GetByKeyAsync(id);
            if (obj == null)
                return NotFound("recurso não encontrado");

            await _suborderService.DeleteAsync(id);
            return Ok($"codigo {id} removido");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ulong id, [FromBody] Suborder suborder)
        {
            var obj = _suborderService.GetByKeyAsync(id);

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