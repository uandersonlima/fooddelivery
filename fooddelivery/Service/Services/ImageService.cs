using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class ImageService : BaseService<Image>, IImageService
    {
        public readonly IImageRepository _repository;
        
        public ImageService(IImageRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}