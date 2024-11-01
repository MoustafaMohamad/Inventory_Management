using Inventory_Management.Common.Enums;

namespace Inventory_Management.Entities
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public productAvailability Available { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Threshold { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}