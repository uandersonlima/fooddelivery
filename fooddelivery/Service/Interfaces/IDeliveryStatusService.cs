using System.Collections.Generic;
using System.Threading.Tasks;
using fooddelivery.Models;

namespace fooddelivery.Service.Interfaces
{
    public interface IDeliveryStatusService:IBaseService<DeliveryStatus>
    {
         bool HasValue();
         void Initialize(List<DeliveryStatus> listStatus);
    }
}