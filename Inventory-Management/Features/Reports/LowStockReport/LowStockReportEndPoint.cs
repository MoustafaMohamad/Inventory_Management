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
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6Im1hcndhIGFkZWwiLCJSb2xlSUQiOiI3IiwiRW1haWwiOiJtYXJ3YS5hc205NUBnbWFpbC5jb20iLCJuYmYiOjE3MzA4MjM3MDUsImV4cCI6MTczMDkxMDEwNCwiaWF0IjoxNzMwODIzNzA1LCJpc3MiOiJGb29kQXBwbGljYXRpb24iLCJhdWQiOiJGb29kQXBwbGljYXRpb25Vc2VycyJ9.LZbdgZJHcxrBx58evc4wp_a9elRaW8gF08gLCNZHtIs
        [HttpGet("{categoryID}")]
        [Authorize]
        public async Task<IActionResult> GetLowStockProductsReportAsync(int categoryID)
        {
            var result = await _mediator.Send(new LowStockReportQuery(categoryID));

            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            var mappedProducts = result.Data.AsQueryable().Map<LowStockReportEndPointResponse>().ToList();
            return Ok(ResultViewModel.Sucess(mappedProducts));
        }
    }
}
