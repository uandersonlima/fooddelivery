using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
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

        public async Task<PaginationList<Address>> GetAllByUserIdAsync(ulong userId, AppView appview)
        {
           return await _repository.GetAllByUserIdAsync(userId, appview);
        }
    }
}