using System.Collections.Generic;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class DeliveryStatusService : BaseService<DeliveryStatus>, IDeliveryStatusService
    {
        public readonly IDeliveryStatusRepository _repository;

        public DeliveryStatusService(IDeliveryStatusRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<bool> HasValue()
        {
            return await _repository.HasValue();
        }

        public async Task StartAsync(List<DeliveryStatus> listStatus)
        {
            await _repository.StartAsync(listStatus);
        }
    }
}