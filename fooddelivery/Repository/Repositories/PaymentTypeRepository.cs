using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Repository.Repositories
{
    public class PaymentTypeRepository : BaseRepository<PaymentType>, IPaymentTypeRepository
    {
        private readonly FoodDeliveryContext _context;
        public PaymentTypeRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}