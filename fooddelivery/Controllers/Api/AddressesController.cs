using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController:ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _addressService.GetByKeyAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var results = await _addressService.GetAllAsync(appview, x => x.City.Contains(appview.Search));
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Address address)
        {
            await _addressService.AddAsync(address);
            return Ok(address);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _addressService.DeleteAsync(id);
            return Ok($"codigo {id} removido");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Address address)
        {
            await _addressService.UpdateAsync(address);
            return Ok(address);
        }
    }
}