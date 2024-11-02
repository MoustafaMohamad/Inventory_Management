namespace Inventory_Management.Features.InventoryTransactions.AddStock
{
    public class AddStockEndPointRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
    }
}
