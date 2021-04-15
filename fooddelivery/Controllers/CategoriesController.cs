using System.Linq;
using System.Threading.Tasks;
using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(int code)
        {
            var result = await _categoryService.GetByKeyAsync(code);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
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
        public async Task<IActionResult> Delete([FromQuery] int code)
        {
            await _categoryService.RemoveAsync(code);
            return Ok($"codigo {code} removido");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            await _categoryService.UpdateAsync(category);
            return Ok(category);
        }
    }
}