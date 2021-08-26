using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Repository.Interfaces
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        Task<PaginationList<Address>> GetAllByUserIdAsync(ulong userId, AppView appview);
        
    }
}