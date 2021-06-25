using System.Collections.Generic;
using System.Linq;
using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Repository.Repositories
{
    public class AddressTypeRepository : BaseRepository<AddressType>, IAddressTypeRepository
    {
        private readonly FoodDeliveryContext _context;

        public AddressTypeRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public bool HasValue()
        {
            return _context.AddressTypes.Any();
        }

        public void Initialize(List<AddressType> listTypes)
        {
            _context.AddressTypes.AddRange(listTypes);
            _context.SaveChanges();
        }
    }
}