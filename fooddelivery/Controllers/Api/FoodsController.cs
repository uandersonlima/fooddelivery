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
    public class FoodsController : ControllerBase
    {
        private readonly IFoodService _foodService;

        public FoodsController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(ulong id)
        {
            var result = await _foodService.GetByKeyAsync(id);
            return Ok(result);
        }

        [HttpGet("ByCategoryId/{categoryid}")]
        public async Task<IActionResult> GetByCategoryId([FromQuery] ulong CategoryId, [FromQuery] AppView appview)
        {
            var results = await _foodService.GetByCategoryIdAsync(CategoryId, appview);
            return Ok(results);
        }

        [HttpGet/*, Authorize(Policy = Policy.EmailVerified)*/]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var results = await _foodService.GetAllAsync(appview, x => x.Name.Contains(appview.Search));
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Food food)
        {
            if (food == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _foodService.AddAsync(food);
            return Ok(food);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] ulong id)
        {
            var obj = _foodService.GetByKeyAsync(id);
            if (obj == null)
                return NotFound("recurso não encontrado");


            await _foodService.DeleteAsync(id);
            return Ok($"codigo {id} removido");
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ulong id, [FromBody] Food food)
        {
            var obj = _foodService.GetByKeyAsync(id);

            if (obj == null)
                return NotFound();

            if (food == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
               
            await _foodService.UpdateAsync(food);
            return Ok(food);
        }

    }
}