using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.InventoryTransactions.AddStock.Orchestrator;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.InventoryTransactions.AddStock
{
    [ApiController]
    [Route("api/InventoryTransactions")]
    public class AddStockEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddStockEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-stock")]
        public async Task<ResultViewModel> AddStockAsunc([FromBody] AddStockEndPointRequest request)
        {
            var result = await _mediator.Send(new AddStockOrchestrator(request.ProductId, request.Quantity, request.UserName));
            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.ErrorCode, result.Message);
            }

            return ResultViewModel.Sucess(result.Data, result.Message);

        }
    }
}
