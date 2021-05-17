using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
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
        public async Task<IActionResult> Get(long id)
        {
            var result = await _foodService.GetByKeyAsync(id);
            return Ok(result);
        }
        
        [HttpGet("ByCategoryId")]
        public async Task<IActionResult> GetByCategoryId([FromRoute]long CategoryId, [FromRoute] AppView appview)
        {
            var results = await _foodService.GetByCategoryIdAsync(CategoryId, appview);
            return Ok(results);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] AppView appview)
        {
            var results = await _foodService.GetAllAsync(appview, x => x.Name.Contains(appview.Search));
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Food food)
        {
            await _foodService.AddAsync(food);
            return Ok(food);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _foodService.DeleteAsync(id);
            return Ok($"codigo {id} removido");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Food food)
        {
            await _foodService.UpdateAsync(food);
            return Ok(food);
        }

    }
}