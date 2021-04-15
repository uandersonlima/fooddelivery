using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class AddressService : BaseService<Address>, IAddressService
    {
        public readonly IAddressRepository _repository;
        
        public AddressService(IAddressRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}