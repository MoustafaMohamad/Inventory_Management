using Microsoft.AspNetCore.SignalR;

namespace Inventory_Management.Common.Helpers
{
    public class SingleRNotificationHub : Hub
    {
        public async Task SendMessage(int ProductID, string ProductName,int Quantity)
        {
            await Clients.All.SendAsync("LowStockMessage", ProductID, ProductName, Quantity);
        }
    }
}
