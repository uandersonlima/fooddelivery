using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class FoodService : BaseService<Food>, IFoodService
    {
        public readonly IFoodRepository _repository;
        
        public FoodService(IFoodRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<PaginationList<Food>> GetByCategoryIdAsync(ulong categoryId, AppView appview)
        {
            return await _repository.GetByCategoryIdAsync(categoryId, appview);
        }
    }
}