using Inventory_Management.Common.Enums;
using Inventory_Management.Entities;

namespace Inventory_Management.Features.InventoryTransactions.GetAllTransaction
{
    public class GetAllTransactionEndPointResponse
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public int UserId { get; set; }
    }
}
