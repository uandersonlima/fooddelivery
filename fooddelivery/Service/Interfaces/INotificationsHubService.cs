using System.Collections.Generic;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Contracts;

namespace fooddelivery.Service.Interfaces
{
    public interface INotificationsHubService
    {
        Task ReportNewPurchaseAsync(Order order, string title = null);
        Task ReportDeliveryStatusForUserAsync(Order order, string title = null);
        Task ReportFoodUpdatesAsync(string title = null,
                                    List<Category> categories = null,
                                    List<Order> orders = null);
        Task ReportFeedbacksUpdatesAsync(string title = null, Feedbacks feedbacks = null);
        
        Task UserIsConnectedAsync(string id);
        Task UserIsDisconnectedAsync(string id);
    }
}