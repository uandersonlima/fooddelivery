using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Constants;
using fooddelivery.Models.Helpers;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = Policy.EmailVerified)]
    public class FoodsController : ControllerBase
    {
        private readonly IFoodService _foodService;

        public FoodsController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> Get(ulong id)
        {
            var result = await _foodService.GetByKeyAsync(id);
            return Ok(result);
        }

        [HttpGet("ByCategoryId/{categoryid}"), AllowAnonymous]
        public async Task<IActionResult> GetByCategoryId([FromQuery] ulong CategoryId, [FromQuery] AppView appview)
        {
            var results = await _foodService.GetByCategoryIdAsync(CategoryId, appview);
            return Ok(results);
        }

        [HttpGet, AllowAnonymous]
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
            var obj = await _foodService.GetByKeyAsync(id);
            if (obj == null)
                return NotFound("recurso não encontrado");


            await _foodService.DeleteAsync(obj);
            return Ok($"codigo {id} removido");
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ulong id, [FromBody] Food food)
        {
            var obj = await _foodService.GetByKeyAsync(id);

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