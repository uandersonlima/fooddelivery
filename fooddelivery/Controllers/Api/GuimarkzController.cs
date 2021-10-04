using System;
using System.Linq;
using System.Threading.Tasks;
using fooddelivery.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuimarkzController : ControllerBase
    {
        private readonly FoodDeliveryContext _context;

        public GuimarkzController(FoodDeliveryContext context)
        {
            _context = context;
        }

        [HttpGet("{type}")]
        public async Task<IActionResult> Commands(string type)
        {
            var texts = await _context.Commands.Where(c => c.Type.ToLower() == type.ToLower()).ToListAsync();
            if (texts.Count > 0)
            {
                var result = texts[new Random().Next(0, texts.Count)];
                return Ok(result);
            }
            else
            {
                return NotFound("Não foi encontrado nada");
            }
        }
    }
}