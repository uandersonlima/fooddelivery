using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Service.Interfaces
{
    public interface IFoodService : IBaseService<Food>
    {
        Task<PaginationList<Food>> GetByCategoryIdAsync(ulong categoryId, AppView appview);
    }
}