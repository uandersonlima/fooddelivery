using System.Device.Location;
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
    [Route(("api/[controller]"))]
    [Authorize(Policy = Policy.EmailVerified)]
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
        [AllowAnonymous]
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

        [HttpGet("distance")]
        public async Task<IActionResult> Distance(ulong AddressId_01, ulong AddressId_02)
        {
            var address_01 = await _addressService.GetByKeyAsync(AddressId_01);
            var address_02 = await _addressService.GetByKeyAsync(AddressId_02);

            if(address_01 == null || address_02 == null)
                return NotFound("Endereços não encontrados");

            if(!address_01.X_coordinate.HasValue || !address_01.Y_coordinate.HasValue 
            || !address_02.X_coordinate.HasValue || !address_02.Y_coordinate.HasValue)
                return BadRequest("Falha na operação, os endereços informados não possuem coordenadas");

            var location_01 = new GeoCoordinate(address_01.X_coordinate.Value, address_01.Y_coordinate.Value);
            var location_02 = new GeoCoordinate(address_02.X_coordinate.Value, address_02.Y_coordinate.Value);

            return Ok(_addressService.CalculateDistanceBetweenLocations(location_01, location_02));
        }
    }
}