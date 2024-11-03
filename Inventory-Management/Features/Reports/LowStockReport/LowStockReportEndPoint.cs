using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management.Features.Reports.LowStockReport.Commands;
using Inventory_Management.Common.Enums;
using Common.Helpers;

namespace Inventory_Management.Features.Reports.LowStockReport
{
    [ApiController]
    [Route("api/reports/lowstockproduct")]
    public class LowStockReportEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public LowStockReportEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{category}")]
        [Authorize]
        public async Task<IActionResult> GetLowStockProductsReportAsync(Category category)
        {
            var result = await _mediator.Send(new LowStockReportQuery(category));

            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            var mappedProducts = result.Data.AsQueryable().Map<LowStockReportEndPointResponse>().ToList();
            return Ok(ResultViewModel.Sucess(mappedProducts));
        }
    }
}
