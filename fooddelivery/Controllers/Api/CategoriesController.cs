using System.Linq;
using System.Threading.Tasks;
using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _categoryService.GetByKeyAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] AppView appview)
        {
            var results = await _categoryService.GetAllAsync(appview, x => x.Name.Contains(appview.Search));
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            await _categoryService.AddAsync(category);
            return Ok(category);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok($"codigo {id} removido");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            await _categoryService.UpdateAsync(category);
            return Ok(category);
        }
    }
}