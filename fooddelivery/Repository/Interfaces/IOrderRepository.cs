using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Repository.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<PaginationList<Order>> GetAllByUserIdAsync(ulong userId, AppView appview);
    }
}