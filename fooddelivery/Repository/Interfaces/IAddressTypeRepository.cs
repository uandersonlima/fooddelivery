using System.Collections.Generic;
using fooddelivery.Models;

namespace fooddelivery.Repository.Interfaces
{
    public interface IAddressTypeRepository : IBaseRepository<AddressType>
    {
        bool HasValue();
        void Initialize(List<AddressType> listTypes);
    }
}