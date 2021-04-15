using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;

namespace fooddelivery.Repository.Repositories
{
    public class ContactRepository: BaseRepository<Contact>, IContactRepository
    {
        private readonly FoodDeliveryContext _context;

        public ContactRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
        }
    }
}