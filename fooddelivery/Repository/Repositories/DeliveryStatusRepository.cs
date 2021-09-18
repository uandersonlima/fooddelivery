using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fooddelivery.Repository.Repositories
{
    public class DeliveryStatusRepository : BaseRepository<DeliveryStatus>, IDeliveryStatusRepository
    {
        private readonly FoodDeliveryContext _context;

        public DeliveryStatusRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public bool HasValue()
        {
            return _context.DeliveryStatus.Any();
        }

        public void Initialize(List<DeliveryStatus> listStatus)
        {
            _context.DeliveryStatus.AddRange(listStatus);
            _context.SaveChanges();
        }
    }
}