using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class ContactService : BaseService<Contact>, IContactService
    {
        public readonly IContactRepository _repository;
        
        public ContactService(IContactRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}