namespace Inventory_Management.Features.Reports.TransactionHistoryReport
{
    public class TransactionHistoryReportEndPointRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProductID { get; set; }
    }
}
