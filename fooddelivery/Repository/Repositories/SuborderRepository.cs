using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;

namespace fooddelivery.Repository.Repositories
{
    public class SuborderRepository : BaseRepository<Suborder>, ISuborderRepository
    {
        private readonly FoodDeliveryContext _context;

        public SuborderRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
        }

    }
}