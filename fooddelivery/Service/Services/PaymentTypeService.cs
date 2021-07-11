using fooddelivery.Models;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class PaymentTypeService: BaseService<PaymentType>, IPaymentTypeService
    {
        public readonly IPaymentTypeRepository _repository;

        public PaymentTypeService(IPaymentTypeRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}