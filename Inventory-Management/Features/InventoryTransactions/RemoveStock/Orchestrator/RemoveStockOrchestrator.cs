using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.InventoryTransactions.Commands;
using Inventory_Management.Features.Common.Products.Commands;
using Inventory_Management.Features.Common.Products.Queries;
using Inventory_Management.Features.Common.Users.Queries;
using MediatR;

namespace Inventory_Management.Features.InventoryTransactions.RemoveStock.Orchestrator
{
    public record RemoveStockOrchestrator(int ProductId, int Quantity, string UserName) : IRequest<ResultDto<bool>>;

    public class RemoveStockOrchestratorHandeler : BaseRequestHandler<InventoryTransaction, RemoveStockOrchestrator, ResultDto<bool>>
    {
        public RemoveStockOrchestratorHandeler(RequestParameters<InventoryTransaction> requestParameters) : base(requestParameters)
        {
        }


        public override async Task<ResultDto<bool>> Handle(RemoveStockOrchestrator request, CancellationToken cancellationToken)
        {
            var isProductExists = await _mediator.Send(new IsProductExists(request.ProductId));

            if (!isProductExists)
            {
                return ResultDto<bool>.Faliure(ErrorCode.NotFound, "This product is not Exists.");
            }

            var result = await _mediator.Send(new GetUserByUserNameQuery(request.UserName));
            if (!result.IsSuccess)
            {
                ResultDto<bool>.Faliure(ErrorCode.NotFound, "This User is not Exists.");
            }

            var DecreaseProductQuantityresult = await _mediator.Send(new DecreaseProductQuantityCommand(request.ProductId, request.Quantity));

            if (!DecreaseProductQuantityresult.IsSuccess)
            {
                ResultDto<bool>.Faliure(ErrorCode.InternalServerError, "Product Quantity is not increased");
            }
            var inventoryTransactionResult = await _mediator.Send(new AddInventoryTransactionsCommand(request.ProductId, request.Quantity, DateTime.Now, TransactionType.AddStock, result.Data.ID));

            if (!inventoryTransactionResult.IsSuccess)
            {
                ResultDto<bool>.Faliure(inventoryTransactionResult.ErrorCode, inventoryTransactionResult.Message);
            }
           await _repository.SaveChangesAsync();
            return ResultDto<bool>.Sucess(true);
        }

    }
}
