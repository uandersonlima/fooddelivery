using fooddelivery.Database;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;

namespace fooddelivery.Repository.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        private readonly FoodDeliveryContext _context;

        public ImageRepository(FoodDeliveryContext context) : base(context)
        {
            _context = context;
        }
    }
}