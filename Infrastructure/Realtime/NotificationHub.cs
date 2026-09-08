using Microsoft.AspNetCore.SignalR;

namespace REAL_ESTATE_CLEAN.Infrastructure.Realtime;

public class NotificationHub : Hub
{
    public async Task SendNotification(int userId, string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", userId, message);
    }
}
