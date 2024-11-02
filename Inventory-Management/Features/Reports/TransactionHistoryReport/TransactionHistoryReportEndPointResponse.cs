using Inventory_Management.Entities;

namespace Inventory_Management.Features.Reports.TransactionHistoryReport
{
    public class TransactionHistoryReportEndPointResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
