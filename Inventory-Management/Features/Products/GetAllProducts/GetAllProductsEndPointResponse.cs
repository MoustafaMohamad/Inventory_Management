using Inventory_Management.Common.Enums;

namespace Inventory_Management.Features.Products.GetAllProducts
{
    public class GetAllProductsEndPointResponse
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Threshold { get; set; }
        public string ImageUrl { get; set; }
    }
}
