using Microsoft.AspNetCore.Mvc;

namespace fooddelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController:ControllerBase
    {
        [HttpPost]
        public IActionResult Get([FromBody] string teste)
        {
            if(!string.IsNullOrEmpty(teste))
            {
             return Ok($"Tudo certo com o {teste.ToString()}");
            }
            else
            {
                return BadRequest($"{teste} é inválido");
            }
        }
    }
}