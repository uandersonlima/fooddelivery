using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;

namespace fooddelivery.Repository.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        private readonly FoodDeliveryContext _context;
        public IngredientRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
        }
    }
}