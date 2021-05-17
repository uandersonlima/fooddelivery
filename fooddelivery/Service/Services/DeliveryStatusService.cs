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

        public bool HasValue()
        {
            return _repository.HasValue();
        }

        public void Initialize(List<DeliveryStatus> listStatus)
        {
            _repository.Initialize(listStatus);
        }
    }
}