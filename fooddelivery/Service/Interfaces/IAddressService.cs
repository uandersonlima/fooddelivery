using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Service.Interfaces
{
    public interface IAddressService : IBaseService<Address>
    {
         Task<PaginationList<Address>> GetAllByUserIdAsync(ulong userId, AppView appview);
    }
}