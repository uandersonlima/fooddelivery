using System.Collections.Generic;

namespace fooddelivery.Models
{
    public class PaymentType
    {
        public ulong Id { get; set; }
        public string Name { get; set; }

        public List<Order> Orders { get; set; }
    }
}