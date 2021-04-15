using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class ChangeService : BaseService<Change>, IChangeService
    {
        public readonly IChangeRepository _repository;
        
        public ChangeService(IChangeRepository repository) : base(repository)
        {
            _repository = repository;
        }
        
    }
}