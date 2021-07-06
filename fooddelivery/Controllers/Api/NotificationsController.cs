using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult NotifyUser(ulong Id)
        {
            return Ok("Client notified");
        }
    }
}