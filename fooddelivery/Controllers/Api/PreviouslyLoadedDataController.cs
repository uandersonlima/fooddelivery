using System.Threading.Tasks;
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
    public class PreviouslyLoadedDataController : ControllerBase
    {
        private readonly IAddressTypeService _typeService;
        private readonly IPaymentTypeService _paymentService;
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

        [HttpGet("getPaymentType")]
        public async Task<IActionResult> GetPaymentType()
        {
            var results = await _paymentService.GetAllAsync(new AppView(), null);
            return Ok(results);
        }

        [HttpGet("getAddressType")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddressType()
        {
            var results = await _typeService.GetAllAsync(new AppView(), null);
            return Ok(results);
        }
    }
}