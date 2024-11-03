using Inventory_Management.Common.Enums;

namespace Inventory_Management.Entities
{
    public class InventoryTransactionArchive : BaseModel
    {

    public int ProductId { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity_inInventory { get; set; }
        public string Unit { get; set; }
        public productAvailability Available { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Threshold { get; set; }
        // public Product Product { get; set; }
        public int Quantity { get; set; }
    public DateTime Date { get; set; }
    public TransactionType TransactionType { get; set; } 
        /// ////////////////////////
    public int UserId { get; set; }
        //public User User { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }

    }
}
