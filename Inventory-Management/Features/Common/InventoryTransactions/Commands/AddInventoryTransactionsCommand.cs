using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.Common.InventoryTransactions.Commands
{
    public record AddInventoryTransactionsCommand(int ProductId, int Quantity, DateTime DateTime,
       TransactionType TransactionType, int UserId) : IRequest<ResultDto<InventoryTransaction>>;

    public class AddInventoryTransactionsCommandHandeler
        : BaseRequestHandler<InventoryTransaction, AddInventoryTransactionsCommand, ResultDto<InventoryTransaction>>
    {
        public AddInventoryTransactionsCommandHandeler(RequestParameters<InventoryTransaction> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<InventoryTransaction>> Handle(AddInventoryTransactionsCommand request, CancellationToken cancellationToken)
        {

            var inventoryTransaction = await _repository.AddAsync(new InventoryTransaction
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Date = request.DateTime,
                UserId = request.UserId,
                TransactionType = request.TransactionType
            });

            if (inventoryTransaction is null)
            {
                ResultDto<InventoryTransaction>.Faliure(ErrorCode.InternalServerError, "An error occurred while saving the entity.");
            }

          await  _repository.SaveChangesAsync();

            return ResultDto<InventoryTransaction>.Sucess(inventoryTransaction);


        }
    }
}
