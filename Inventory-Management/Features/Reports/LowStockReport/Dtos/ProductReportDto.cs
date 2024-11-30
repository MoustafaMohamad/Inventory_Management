using Inventory_Management.Common.Enums;

namespace Inventory_Management.Features.Reports.LowStockReport.Dtos
{
    public class ProductReportDto
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string Available { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Threshold { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
