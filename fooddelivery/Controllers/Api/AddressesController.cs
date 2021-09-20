using System;
using System.Device.Location;
using System.Linq;
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
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;

        public AddressesController(IAddressService addressService, IAuthService authService, IOrderService orderService)
        {
            _addressService = addressService;
            _authService = authService;
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(ulong id)
        {
            var result = await _addressService.GetByKeyAsync(id);
            if (result == null)
                return NotFound();

            var isAdmin = HttpContext.User.IsInRole(Policy.Admin);
            var loggedInUser = await _authService.GetLoggedUserAsync();

            if (!isAdmin && result.UserId != loggedInUser.Id)
            {
                return Unauthorized("Você não tem permissão para ver esse endereço");
            }
            return Ok(result);
        }

        [HttpGet("AllByUserId/{userId}")]
        public async Task<IActionResult> GetAll(ulong userId, [FromQuery] AppView appview)
        {
            var results = await _addressService.GetAllByUserIdAsync(userId, appview);
            if (results.Count > 0)
            {
                var isAdmin = HttpContext.User.IsInRole(Policy.Admin);
                var loggedInUser = await _authService.GetLoggedUserAsync();
                if (!isAdmin && results[0].UserId != loggedInUser.Id)
                {
                    return Unauthorized("Você não tem permissão para ver esse endereço");
                }
            }
            return Ok(results);
        }

        [HttpGet]
        [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> GetAll([FromQuery] AppView appview)
        {
            var results = await _addressService.GetAllAsync(appview, x => x.City.Contains(appview.Search)
                                                                       && x.Neighborhood.Contains(appview.Search)
                                                                       && x.State.Contains(appview.Search));

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
        [AllowAnonymous]
        public async Task<IActionResult> Delete([FromQuery] ulong id)
        {
            var obj = await _addressService.GetByKeyAsync(id);

            if (obj == null)
                return NotFound("recurso não encontrado");
            if (obj.isDeleted)
                return NotFound("recurso não encontrado");

            var orderQuantity = await _orderService.GetAllByAddressIdAsync(id, new AppView());

            if (orderQuantity.Count > 0)
            {
                obj.DeleteDate = DateTime.Now;
                obj.isDeleted = true;
                await _addressService.UpdateAsync(obj);
            }
            else
            {
                await _addressService.DeleteAsync(obj);
            }
            return Ok($"codigo {id} removido");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ulong id, [FromBody] Address address)
        {

            if (id != address.Id)
                return BadRequest("Id mismatched: " + id);

            var obj = await _addressService.GetByKeyAsync(id);

            if (obj == null)
                return NotFound("Conteúdo não encontrado");

            if (obj.isDeleted)
                return BadRequest("Não é possível atualizar esse endereço, pois ele já foi logicamente deletado");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            obj.Number = address.Number;
            obj.City = address.City;
            obj.Neighborhood = address.Neighborhood;
            obj.State = address.State;
            obj.Addon = address.Addon;
            obj.AddressTypeId = address.AddressTypeId;
            obj.X_coordinate = address.X_coordinate;
            obj.Y_coordinate = address.Y_coordinate;

            await _addressService.UpdateAsync(obj);

            return Ok(obj);

        }

        [HttpGet("distance")]
        public async Task<IActionResult> Distance(ulong AddressId_01, ulong AddressId_02)
        {
            var address_01 = await _addressService.GetByKeyAsync(AddressId_01);
            var address_02 = await _addressService.GetByKeyAsync(AddressId_02);

            if (address_01 == null && address_02 == null)
                return NotFound("Endereços não encontrados");

            if (!address_01.X_coordinate.HasValue && !address_01.Y_coordinate.HasValue
            && !address_02.X_coordinate.HasValue && !address_02.Y_coordinate.HasValue)
                return BadRequest("Falha na operação, os endereços informados não possuem coordenadas");

            var location_01 = new GeoCoordinate(address_01.X_coordinate.Value, address_01.Y_coordinate.Value);
            var location_02 = new GeoCoordinate(address_02.X_coordinate.Value, address_02.Y_coordinate.Value);

            return Ok(_addressService.CalculateDistanceBetweenLocations(location_01, location_02));
        }

        [HttpPut("standardAddress/{AddressId}")]
        public async Task<IActionResult> StandardAddress(ulong AddressId)
        {
            var loggedInUser = await _authService.GetLoggedUserAsync();
            var listAddress = await _addressService.GetAllByUserIdAsync(loggedInUser.Id, new AppView());
            var succeeded = listAddress.Any(address => address.Id == AddressId);

            if (succeeded)
                listAddress.ForEach(address => address.Standard = address.Id == AddressId ? true : false);
            else
                return BadRequest("Não foi possível alterar o endereço informado como principal, verifique se o endereço informado corresponde ao usuário solicitante!");

            loggedInUser.Addresses = null; /*Remove erro de tracking*/
            await _addressService.UpdateRangeAsync(listAddress);

            return Ok(listAddress.Select(address => new Address
            {
                Id = address.Id,
                Number = address.Number,
                City = address.City,
                Neighborhood = address.Neighborhood,
                State = address.State,
                Addon = address.Addon,
                Standard = address.Standard,
                AddressTypeId = address.AddressTypeId,
                X_coordinate = address.X_coordinate,
                Y_coordinate = address.Y_coordinate,
                UserId = address.UserId

            }).Where(address => address.Id == AddressId).FirstOrDefault());
        }
    }
}