using System.Collections.Generic;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Database
{
    public class SeedData
    {
        private readonly IDeliveryStatusService _status;

        public SeedData(IDeliveryStatusService status)
        {
            _status = status;
        }

        public void Initialize()
        {
            if (!_status.HasValue())
            {
                 _status.Initialize(new List<DeliveryStatus>{
                    new DeliveryStatus {Id = 1, Name = "Aberto"},
                    new DeliveryStatus {Id = 2, Name = "Em progresso"},
                    new DeliveryStatus {Id = 3, Name = "Pronto"},
                    new DeliveryStatus {Id = 4, Name = "Saiu para entrega"},
                    new DeliveryStatus {Id = 5, Name = "Entregue"},
                    new DeliveryStatus {Id = 6, Name = "Não entregue"},
                    new DeliveryStatus {Id = 7, Name = "Cancelado"}
                });
            }
        }
    }
}