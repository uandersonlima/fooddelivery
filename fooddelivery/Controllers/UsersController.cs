using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Tudo certo");
        }
    }
}