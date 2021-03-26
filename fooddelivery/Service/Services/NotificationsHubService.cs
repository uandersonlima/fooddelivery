using fooddelivery.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace fooddelivery.Service.Services
{
    public class NotificationsHubService: Hub<INotificationsHubService>
    {
        
    }
}