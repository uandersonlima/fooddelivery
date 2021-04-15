using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class SuborderService : BaseService<Suborder>, ISuborderService
    {
        public readonly ISuborderRepository _repository;
        
        public SuborderService(ISuborderRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}