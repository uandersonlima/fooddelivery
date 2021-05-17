using System.Collections.Generic;

namespace fooddelivery.Models
{
    public class DeliveryStatus
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<Order> Orders { get; set; }
        
    }
}