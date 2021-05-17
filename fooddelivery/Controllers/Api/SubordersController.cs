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
        public async Task<IActionResult> Get(long id)
        {
            var result = await _suborderService.GetByKeyAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] AppView appview)
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
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _suborderService.DeleteAsync(id);
            return Ok($"codigo {id} removido");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Suborder suborder)
        {
            await _suborderService.UpdateAsync(suborder);
            return Ok(suborder);
        }
    }
}