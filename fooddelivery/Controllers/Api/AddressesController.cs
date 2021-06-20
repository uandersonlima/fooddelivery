using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route(("api/[controller]"))]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(ulong id)
        {
            var result = await _addressService.GetByKeyAsync(id);
            return Ok(result);
        }

        [HttpGet("AllByUserId/{userId}")]
        public async Task<IActionResult> GetAll(ulong userId, [FromQuery] AppView appview)
        {
            var results = await _addressService.GetAllByUserIdAsync(userId, appview);
            return Ok(results);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var results = await _addressService.GetAllAsync(appview, x => x.City.Contains(appview.Search)
                                                                       || x.Neighborhood.Contains(appview.Search)
                                                                       || x.State.Contains(appview.Search));

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Address address)
        {
            if (address == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _addressService.AddAsync(address);
            return Ok(address);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] ulong id)
        {
            var obj = await _addressService.GetByKeyAsync(id);
            if (obj == null)
                return NotFound("recurso não encontrado");

            await _addressService.DeleteAsync(obj);
            return Ok($"codigo {id} removido");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ulong id, [FromBody] Address address)
        {

            var obj = await _addressService.GetByKeyAsync(id);

            if (obj == null)
                return NotFound();

            if (address == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            
            await _addressService.UpdateAsync(address);
            return Ok(address);
        }
    }
}