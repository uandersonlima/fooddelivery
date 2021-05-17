using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers.Api
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _ingredientService.GetByKeyAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] AppView appview)
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
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _ingredientService.DeleteAsync(id);
            return Ok($"codigo {id} removido");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Ingredient ingredient)
        {
            await _ingredientService.UpdateAsync(ingredient);
            return Ok(ingredient);
        }
    }
}