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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(ulong id)
        {
            var result = await _categoryService.GetByKeyAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var results = await _categoryService.GetAllAsync(appview, x => x.Name.Contains(appview.Search));
            return Ok(results);
        }

        [HttpPost]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            if (category == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _categoryService.AddAsync(category);
            return Ok(category);
        }

        [HttpDelete]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Delete([FromQuery] ulong id)
        {

            var obj = await _categoryService.GetByKeyAsync(id);
            if (obj == null)
                return NotFound("recurso não encontrado");


            await _categoryService.DeleteAsync(obj);

            return Ok($"codigo {id} removido");
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Update(ulong id, [FromBody] Category category)
        {
            if (id != category.Id)
                return BadRequest("Id mismatched: " + id);

            var obj = await _categoryService.GetByKeyAsync(id);

            if (obj == null)
                return NotFound("Conteúdo não encontrado");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            obj.Name = category.Name;

            await _categoryService.UpdateAsync(obj);

            return Ok(obj);
        }
    }
}