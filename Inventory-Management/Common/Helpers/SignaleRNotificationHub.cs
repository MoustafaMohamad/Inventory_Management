using Microsoft.AspNetCore.SignalR;

namespace Inventory_Management.Common.Helpers
{
    public class SignaleRNotificationHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("LowStockMessage", user, message);
        }
    }
}
