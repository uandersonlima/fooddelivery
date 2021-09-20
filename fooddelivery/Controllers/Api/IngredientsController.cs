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
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> Get(ulong id)
        {
            var result = await _ingredientService.GetByKeyAsync(id);
            return Ok(result);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var results = await _ingredientService.GetAllAsync(appview, x => x.Name.Contains(appview.Search));
            return Ok(results);
        }

        [HttpPost]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Create([FromBody] Ingredient ingredient)
        {
            if (ingredient == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _ingredientService.AddAsync(ingredient);
            return Ok(ingredient);
        }

        [HttpDelete]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Delete([FromQuery] ulong id)
        {
            var obj = await _ingredientService.GetByKeyAsync(id);
            if (obj == null)
                return NotFound("recurso não encontrado");

            await _ingredientService.DeleteAsync(obj);
            return Ok($"codigo {id} removido");
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Update(ulong id, [FromBody] Ingredient ingredient)
        {
            if (id != ingredient.Id)
                return BadRequest("Id mismatched: " + id);

            var obj = await _ingredientService.GetByKeyAsync(id);

            if (obj == null)
                return NotFound("Conteúdo não encontrado");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            obj.Name = ingredient.Name;
            obj.Price = ingredient.Price;
            obj.Unity = ingredient.Unity;

            await _ingredientService.UpdateAsync(obj);
            return Ok(obj);
        }
    }
}