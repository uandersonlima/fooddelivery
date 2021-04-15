using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(int code)
        {
            var result = await _ingredientService.GetByKeyAsync(code);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var results = await _ingredientService.GetAllAsync(appview, x => x.Name.Contains(appview.Search));
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ingredient ingredient)
        {
            await _ingredientService.AddAsync(ingredient);
            return Ok(ingredient);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int code)
        {
            await _ingredientService.RemoveAsync(code);
            return Ok($"codigo {code} removido");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Ingredient ingredient)
        {
            await _ingredientService.UpdateAsync(ingredient);
            return Ok(ingredient);
        }
    }
}