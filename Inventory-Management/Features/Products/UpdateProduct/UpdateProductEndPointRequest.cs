using Inventory_Management.Common.Enums;

namespace Inventory_Management.Features.Products.UpdateProduct
{
    public class UpdateProductEndPointRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public productAvailability Available { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Threshold { get; set; }
        public IFormFile Image { get; set; }
    }
}
