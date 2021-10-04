using System;
using System.Linq;
using System.Threading.Tasks;
using fooddelivery.Database;
using fooddelivery.Models;
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

        [HttpPost]
        public async Task<IActionResult> Create(string type, string phrase)
        {
            var serverPhrase = new Guimarkz_commands { Type = type, Phrase = phrase };
            _context.Commands.Add(serverPhrase);
            await _context.SaveChangesAsync();

            return Ok(serverPhrase);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ulong Id)
        {
            var serverPhrase = await _context.Commands.FindAsync(Id);
            _context.Remove(serverPhrase);
            await _context.SaveChangesAsync();

            return Ok($"Phrase {Id} removida");
        }


        [HttpGet]
        public async Task<IActionResult> All(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return Ok(await _context.Commands.ToListAsync());
            }
            else
            {
                return Ok(await _context.Commands.Where(command => command.Type.Contains(type)).ToListAsync());
            }
        }
    }
}