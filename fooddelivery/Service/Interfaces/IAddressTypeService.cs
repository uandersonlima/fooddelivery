using System.Collections.Generic;
using fooddelivery.Models;

namespace fooddelivery.Service.Interfaces
{
    public interface IAddressTypeService : IBaseService<AddressType>
    {
        bool HasValue();
        void Initialize(List<AddressType> listTypes);
    }
}