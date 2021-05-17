using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Repository.Interfaces
{
    public interface IFoodRepository : IBaseRepository<Food>
    {
         Task<PaginationList<Food>> GetByCategoryIdAsync(long categoryId, AppView appview);
    }
}