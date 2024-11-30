namespace Inventory_Management.Entities
{
    public class InventoryTransaction : BaseModel
    {
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; }
    public TransactionType TransactionType { get; set; } 
    public int UserId { get; set; }
    public User User { get; set; }
    }
}
