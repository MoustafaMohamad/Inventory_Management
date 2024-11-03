using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common.Repositories;
using Inventory_Management.Entities;
using Inventory_Management.Features.Reports.TransactionHistoryReport.Dtos;
using MediatR;

namespace Inventory_Management.Features.Reports.TransactionHistoryReport.Queries
{
    public record TransactionHistoryReportQuery(DateTime StartDate,DateTime EndDate,int ProductID) : IRequest<ResultDto<IEnumerable<TransactionReportDto>>>;
    public class TransactionHistoryReportQueryHandler : BaseRequestHandler<InventoryTransaction, TransactionHistoryReportQuery, ResultDto<IEnumerable<TransactionReportDto>>>
    {
        public TransactionHistoryReportQueryHandler(RequestParameters<InventoryTransaction> requestParameters) : base(requestParameters)
        {
            
        }
        public async override Task<ResultDto<IEnumerable<TransactionReportDto>>> Handle(TransactionHistoryReportQuery request, CancellationToken cancellationToken)
        {

            var transactionsResult = await _repository.Get(t=>t.Date>= request.StartDate && t.Date<= request.EndDate && t.ProductId==request.ProductID);
            if (!transactionsResult.Any()) 
            {
                return ResultDto<IEnumerable<TransactionReportDto>>.Faliure(ErrorCode.NoTransactionFound, "No Transactions Found");
            }
            var transactions = transactionsResult.Map<TransactionReportDto>().ToList();
            return ResultDto<IEnumerable<TransactionReportDto>>.Sucess(transactions);
        }
    }
}
