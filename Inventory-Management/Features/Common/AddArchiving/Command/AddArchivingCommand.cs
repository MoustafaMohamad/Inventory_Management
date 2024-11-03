using Inventory_Management.Common;
using Inventory_Management.Common.Enums;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.InventoryTransactions.Commands;
using Inventory_Management.Features.Common.Products.Commands;
using Inventory_Management.Features.Common.Products.Queries;
using Inventory_Management.Features.Common.Users.Queries;
using MediatR;

namespace Inventory_Management.Features.Common.AddArchiving.Command
{
    public record AddArchivingCommand(InventoryTransactionArchive archive) : IRequest<ResultDto<bool>>;
    

    public class AddArchivingCommandHandeler : BaseRequestHandler<InventoryTransactionArchive, AddArchivingCommand, ResultDto<bool>>
    {
        public AddArchivingCommandHandeler(RequestParameters<InventoryTransactionArchive> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<bool>> Handle(AddArchivingCommand request, CancellationToken cancellationToken)
        {
            var isProductExists = await _mediator.Send(new IsProductExists(request.archive.ProductId));

            if (!isProductExists)
            {
                return ResultDto<bool>.Faliure(ErrorCode.NotFound, "This product is not Exists.");
            }

            var Userresult = await _mediator.Send(new GetUserByUserNameQuery(request.archive.UserName));
            if (!Userresult.IsSuccess)
            {
                ResultDto<bool>.Faliure(ErrorCode.NotFound, "This User is not Exists.");
            }

           
            var inventoryTransaction = await _repository.AddAsync(new InventoryTransactionArchive
            {
                ProductId = request.archive.ProductId,
                Quantity = request.archive.Quantity,
                Date = request.archive.Date,
                UserId = request.archive.UserId,
                TransactionType = request.archive.TransactionType,
                Name = request.archive.Name,
                Category =request.archive.Category ,
                Price=request.archive.Price,
                Quantity_inInventory=request.archive.Quantity_inInventory,
                Unit=request.archive.Unit,
                Available=request.archive.Available,
                ExpiryDate=request.archive.ExpiryDate,
                Threshold=request.archive.Threshold,
                RoleID = request.archive.RoleID,
                UserName =request.archive.UserName,
                Password =request.archive.Password,
                Email = request.archive.Email 

    }) ;

            //var inventoryTransactionArchiveResult = await   _repository.AddAsync (inventoryTransaction);
            

            await _repository.SaveChangesAsync();

            return ResultDto<bool>.Sucess(true);
        }
    }

}
