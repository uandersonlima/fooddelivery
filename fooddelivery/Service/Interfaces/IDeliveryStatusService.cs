using System.Collections.Generic;
using System.Threading.Tasks;
using fooddelivery.Models;

namespace fooddelivery.Service.Interfaces
{
    public interface IDeliveryStatusService:IBaseService<DeliveryStatus>
    {
         Task<bool> HasValue();
         Task StartAsync(List<DeliveryStatus> listStatus);
    }
}