using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;

namespace fooddelivery.Repository.Repositories
{
    public class FoodRepository: BaseRepository<Food>, IFoodRepository
    {
        private readonly FoodDeliveryContext _context;

        public FoodRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
        }
    }
}