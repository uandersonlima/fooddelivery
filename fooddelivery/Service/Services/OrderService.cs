using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Helpers;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        public readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<PaginationList<Order>> GetAllByAddressIdAsync(ulong addressId, AppView appview)
        {
            return await _repository.GetAllByAddressIdAsync(addressId, appview);
        }

        public async Task<PaginationList<Order>> GetAllByUserIdAsync(ulong userId, AppView appview)
        {
            return await _repository.GetAllByUserIdAsync(userId, appview);
        }
    }
}