using System.Collections.Generic;
using System.Device.Location;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Service.Interfaces
{
    public interface IAddressService : IBaseService<Address>
    {
        object CalculateDistanceBetweenLocations(GeoCoordinate location_01, GeoCoordinate location_02);
        Task<PaginationList<Address>> GetAllByUserIdAsync(ulong userId, AppView appview);
        Task UpdateRangeAsync(List<Address> addresses);
    }
}