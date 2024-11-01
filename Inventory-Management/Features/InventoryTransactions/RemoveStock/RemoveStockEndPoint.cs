using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.InventoryTransactions.AddStock.Orchestrator;
using Inventory_Management.Features.InventoryTransactions.RemoveStock.Orchestrator;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.InventoryTransactions.RemoveStock
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RemoveStockEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public RemoveStockEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Remove-stock")]
        public async Task<ResultViewModel> RemoveStockAsunc([FromBody] RemoveStockEndPointRequest request)
        {
            var result = await _mediator.Send(new RemoveStockOrchestrator(request.ProductId, request.Quantity, request.UserName));
            if (!result.IsSuccess)
            {
                return ResultViewModel.Faliure(result.ErrorCode, result.Message);
            }

            return ResultViewModel.Sucess(result.Data, result.Message);
        }
    }
}
