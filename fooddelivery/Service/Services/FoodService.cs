using fooddelivery.Models;
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
    }
}