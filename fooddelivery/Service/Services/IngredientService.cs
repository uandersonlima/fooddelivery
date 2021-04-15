using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class IngredientService : BaseService<Ingredient>, IIngredientService
    {
        public readonly IIngredientRepository _repository;

        public IngredientService(IIngredientRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}