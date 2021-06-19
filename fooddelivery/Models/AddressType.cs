using System.Collections.Generic;

namespace fooddelivery.Models
{
    public class AddressType
    {
        public ulong Id { get; set; }
        public string Name { get; set; }

        public List<Address> Orders { get; set; }
    }
}