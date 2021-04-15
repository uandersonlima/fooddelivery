using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers
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

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(int code)
        {
            var result = await _foodService.GetByKeyAsync(code);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
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
        public async Task<IActionResult> Delete([FromQuery] int code)
        {
            await _foodService.RemoveAsync(code);
            return Ok($"codigo {code} removido");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Food food)
        {
            await _foodService.UpdateAsync(food);
            return Ok(food);
        }
    }
}