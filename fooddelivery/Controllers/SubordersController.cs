using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers
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

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(int code)
        {
            var result = await _suborderService.GetByKeyAsync(code);
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
            await _suborderService.AddAsync(suborder);
            return Ok(suborder);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int code)
        {
            await _suborderService.RemoveAsync(code);
            return Ok($"codigo {code} removido");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Suborder suborder)
        {
            await _suborderService.UpdateAsync(suborder);
            return Ok(suborder);
        }
    }
}