using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;

namespace fooddelivery.Repository.Repositories
{
    public class OrderRepository: BaseRepository<Order>, IOrderRepository
    {
        private readonly FoodDeliveryContext _context;

        public OrderRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
        }
        
    }
}