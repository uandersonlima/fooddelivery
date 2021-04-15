using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(int code)
        {
            var result = await _imageService.GetByKeyAsync(code);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var results = await _imageService.GetAllAsync(appview, x => x.Name.Contains(appview.Search));
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Image image)
        {
            await _imageService.AddAsync(image);
            return Ok(image);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int code)
        {
            await _imageService.RemoveAsync(code);
            return Ok($"codigo {code} removido");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Image image)
        {
            await _imageService.UpdateAsync(image);
            return Ok(image);
        }
    }
}