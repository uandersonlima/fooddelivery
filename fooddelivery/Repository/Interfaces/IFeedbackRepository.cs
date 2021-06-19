using System.Threading.Tasks;
using fooddelivery.Models.Contracts;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Repository.Interfaces
{
    public interface IFeedbackRepository : IBaseRepository<Feedbacks>
    {
         Task<PaginationList<Feedbacks>> GetAllByUserIdAsync(ulong userId, AppView appview);
    }
}