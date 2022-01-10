using System.Collections.Generic;
using System.Threading.Tasks;
using fooddelivery.Models;
using fooddelivery.Models.Contracts;

namespace fooddelivery.Service.Interfaces
{
    public interface INotificationsHubService
    {
        Task ReportNewPurchaseAsync(string title = null);
        Task ReportDeliveryStatusForUserAsync(string title = null);
        Task ReportFoodUpdatesAsync(string title = null);
        Task ReportFeedbacksUpdatesAsync(string title = null);
        
        Task UserIsConnectedAsync(ulong id);
        Task UserIsDisconnectedAsync(ulong id);

    }
}