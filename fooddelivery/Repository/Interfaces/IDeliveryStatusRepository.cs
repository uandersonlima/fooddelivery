using System.Collections.Generic;
using System.Threading.Tasks;
using fooddelivery.Models;

namespace fooddelivery.Repository.Interfaces
{
    public interface IDeliveryStatusRepository : IBaseRepository<DeliveryStatus>
    {
        Task<bool> HasValue();
        Task StartAsync(List<DeliveryStatus> listStatus);
    }
}