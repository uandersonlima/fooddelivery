using fooddelivery.Models;
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
    }
}