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
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> Get(ulong id)
        {
            var result = await _imageService.GetByKeyAsync(id);
            return Ok(result);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var results = await _imageService.GetAllAsync(appview, x => x.Name.Contains(appview.Search));
            return Ok(results);
        }

        [HttpPost, RequestSizeLimit(100_000_000)]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Create([FromBody] Image image)
        {
            if (image == null)
                return BadRequest("imagem inválida");
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _imageService.AddAsync(image);
            return Ok(image);
        }

        [HttpDelete]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Delete([FromQuery] ulong id)
        {
            var obj = await _imageService.GetByKeyAsync(id);
            if (obj == null)
                return NotFound("recurso não encontrado");

            await _imageService.DeleteAsync(obj);
            return Ok($"codigo {id} removido");
        }

        [HttpPut("{id}"), RequestSizeLimit(100_000_000)]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> Update(ulong id, [FromBody] Image image)
        {
            if (id != image.Id)
                return BadRequest("Id mismatched: " + id);

            var obj = await _imageService.GetByKeyAsync(id);

            if (obj == null)
                return NotFound("Conteúdo não encontrado");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            obj.Name = image.Name;
            obj.Type = image.Type;
            obj.Data = image.Data;
            obj.Size = image.Size;
            obj.FoodId = image.FoodId;

            await _imageService.UpdateAsync(obj);

            return Ok(obj);
        }
    }
}