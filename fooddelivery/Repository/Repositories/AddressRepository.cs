using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;

namespace fooddelivery.Repository.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        private readonly FoodDeliveryContext _context;

        public AddressRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
        }
    }
}