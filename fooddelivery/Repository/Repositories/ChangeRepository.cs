using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;

namespace fooddelivery.Repository.Repositories
{
    public class ChangeRepository: BaseRepository<Change>, IChangeRepository
    {
        private readonly FoodDeliveryContext _context;

        public ChangeRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
        }
    }
}