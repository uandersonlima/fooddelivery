using System.Threading.Tasks;
using fooddelivery.Models.Contracts;
using fooddelivery.Models.Helpers;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Service.Interfaces;

namespace fooddelivery.Service.Services
{
    public class FeedbackService : BaseService<Feedbacks>, IFeedbackService
    {
        public readonly IFeedbackRepository _repository;

        public FeedbackService(IFeedbackRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<PaginationList<Feedbacks>> GetAllByUserIdAsync(ulong userId, AppView appview)
        {
            return await _repository.GetAllByUserIdAsync(userId, appview);
        }
        
    }
}