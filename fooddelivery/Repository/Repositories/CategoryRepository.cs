using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;

namespace fooddelivery.Repository.Repositories
{
    public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
    {
        private readonly FoodDeliveryContext _context;

        public CategoryRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
        }
    }
}