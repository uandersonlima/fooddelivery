using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Constants;
using fooddelivery.Models.Contracts;
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
        [Authorize(Policy = Policy.Admin)]
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
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Delete([FromQuery] ulong id)
        {
            var obj = await _foodService.GetByKeyAsync(id);
            if (obj == null)
                return NotFound("recurso não encontrado");


            await _foodService.DeleteAsync(obj);
            return Ok($"codigo {id} removido");
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Policy.Admin)]
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

        [HttpPut("addingredient/{id}")]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> AddIngredients(ulong id, [FromBody] List<ulong> ingredientIds, [FromServices] IIngredientService _ingredientService)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var food = await _foodService.GetByKeyAsync(id);
            if (food == null)
                return NotFound();

            if (ingredientIds.Count == 0)
                return BadRequest("Informe os identificadores dos ingredientes");

            var errors = new StringBuilder();

            ingredientIds.ForEach(ingredientId =>
            {
                if (ingredientId == 0)
                {
                    errors.AppendLine($"identificador {ingredientId}, na posição {ingredientIds.IndexOf(ingredientId)} inválido!");
                }
                else
                {
                    var ingredient = _ingredientService.GetByKeyAsync(ingredientId).Result;
                    if (ingredient == null)
                        errors.AppendLine($"ingreditent referente ao identificador {ingredientId} não foi encontrado!");
                }

            });

            if (errors.Length != 0)
                return BadRequest(errors.ToString());

            food.AddIngredient(ingredientIds);

            await _foodService.UpdateAsync(food);
            return Ok(food);
        }

        [HttpPut("removeingredient/{id}")]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> RemoveIngredients(ulong id, [FromBody] List<ulong> ingredientIds, [FromServices] IIngredientService _ingredientService)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var food = await _foodService.GetByKeyAsync(id);
            if (food == null)
                return NotFound();

            if (ingredientIds.Count == 0)
                return BadRequest("Informe os identificadores dos ingredientes");

            var errors = new StringBuilder();
            ingredientIds.ForEach(ingredientId =>
            {
                if (ingredientId == 0)
                {
                    errors.Append($"identificador {ingredientId} inválido!");
                }
                else
                {
                    var ingredient = _ingredientService.GetByKeyAsync(ingredientId).Result;
                    if (ingredient == null)
                        errors.Append($"ingreditent referente ao identificador {ingredientId} não foi encontrado!");
                }

            });

            if (errors.Length != 0)
                return BadRequest(errors.ToString());

            food.RemoveIngredient(ingredientIds);

            await _foodService.UpdateAsync(food);
            return Ok(food);
        }
    }
}