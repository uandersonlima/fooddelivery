using System.Collections.Generic;
using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class AddressTypeService :  BaseService<AddressType>, IAddressTypeService
    {
        public readonly IAddressTypeRepository _repository;

        public AddressTypeService(IAddressTypeRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public bool HasValue()
        {
            return _repository.HasValue();
        }

        public void Initialize(List<AddressType> listTypes)
        {
            _repository.Initialize(listTypes);
        } 
        
    }
}