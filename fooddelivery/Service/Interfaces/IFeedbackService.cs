using System.Threading.Tasks;
using fooddelivery.Models.Contracts;
using fooddelivery.Models.Helpers;

namespace fooddelivery.Service.Interfaces
{
    public interface IFeedbackService: IBaseService<Feedbacks>
    {
         Task<Feedbacks> GetByKeyAsync(ulong userId, ulong orderId);
         Task<PaginationList<Feedbacks>> GetAllByUserIdAsync(ulong userId, AppView appview);   
    }
}