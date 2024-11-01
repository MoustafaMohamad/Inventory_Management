namespace Inventory_Management.Features.InventoryTransactions.RemoveStock
{
    public class RemoveStockEndPointRequest
    {
        
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public string UserName { get; set; }
        
    }
}
