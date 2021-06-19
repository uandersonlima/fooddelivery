using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Service.Interfaces
{
    public interface IOrderService : IBaseService<Order>
    {
         Task<PaginationList<Order>> GetAllByUserIdAsync(ulong userId, AppView appview);
    }
}