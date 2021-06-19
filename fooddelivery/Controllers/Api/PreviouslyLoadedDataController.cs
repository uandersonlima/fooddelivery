using System.Threading.Tasks;
using fooddelivery.Models.Helpers;
using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreviouslyLoadedDataController : ControllerBase
    {
        private readonly IAddressTypeService _typeService;
        private readonly IDeliveryStatusService _statusService;

        public PreviouslyLoadedDataController(IAddressTypeService typeService, IDeliveryStatusService statusService)
        {
            _typeService = typeService;
            _statusService = statusService;
        }

        [HttpGet("getDeliveryStatus")]
        public async Task<IActionResult> GetDeliveryStatus()
        {
            var results = await _statusService.GetAllAsync(new AppView(), null);
            return Ok(results);
        }

        [HttpGet("getAddressType")]
        public async Task<IActionResult> GetAddressType()
        {
            var results = await _typeService.GetAllAsync(new AppView(), null);
            return Ok(results);
        }
    }
}