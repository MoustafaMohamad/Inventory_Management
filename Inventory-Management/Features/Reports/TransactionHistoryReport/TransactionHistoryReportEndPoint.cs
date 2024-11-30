using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Reports.TransactionHistoryReport.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Reports.TransactionHistoryReport
{
    [ApiController]
    [Route("api/reports/transactionhistory")]
    public class TransactionHistoryReportEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionHistoryReportEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTransactionHistoryReportAsync(DateTime StartDate, DateTime EndDate, int ProductID)
        {
            var result = await _mediator.Send(new TransactionHistoryReportQuery(StartDate, EndDate, ProductID));

            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            var mappedTransactionss = result.Data.AsQueryable().Map<TransactionHistoryReportEndPointResponse>().ToList();
            return Ok(ResultViewModel.Sucess(mappedTransactionss));
        }
    }
}
