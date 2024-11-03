using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Features.Common.GetAllArchiveTransaction.Queries;
using MediatR;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.AddArchiving.Command;
using Azure.Core;
using Inventory_Management.Features.Common.Products.Queries;
using Inventory_Management.Features.Common.Users.Queries;
using Inventory_Management.Features.InventoryTransactions.RemoveStock.Orchestrator;
using Inventory_Management.Features.Common.DeleteProduct.Commands;



namespace Inventory_Management.Features.Common.BackGround_jobs
{
    public class SampleJob2
    {
        private readonly IMediator _mediator;
        public SampleJob2(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task ExecuteJobAsync()
        {
            // Job logic here
            var result = await _mediator.Send(new GetAllArchiveTransactionQuery());


            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            var mappedTransactions = result.Data.AsQueryable().ToList();
            foreach (var transaction in mappedTransactions)
            {
                if (transaction.Date <= DateTime.Now.AddYears(-1))
                {


                    var product = await _mediator.Send(new GetTransactionQuery(transaction.ProductId));

                    var user = await _mediator.Send(new GetUserQuery(transaction.UserId));


                    var inventoryTransaction = new InventoryTransactionArchive
                    {
                        ProductId = transaction.ProductId,
                        Quantity = transaction.Quantity,
                        Date = transaction.Date,
                        UserId = transaction.UserId,
                        TransactionType = transaction.TransactionType,
                        Name = product.Data.Name,
                        Category = product.Data.Category,
                        Price = product.Data.Price,
                        Quantity_inInventory = product.Data.Quantity,
                        Unit = product.Data.Name,
                        Available = product.Data.Available,
                        ExpiryDate = product.Data.ExpiryDate,
                        Threshold = product.Data.Threshold,
                        RoleID = transaction.User.RoleID,
                        UserName = user.Data.UserName,
                        Password = user.Data.Password,
                        Email = user.Data.Email

                    };


                    
                    var ee = await _mediator.Send(new AddArchivingCommand(inventoryTransaction));

                    if (!ee.IsSuccess)
                    {
                        throw new BusinessException(ee.ErrorCode, ee.Message);
                    }



                     await _mediator.Send(new DeleteTransactionByIdCommand(transaction.ID));

                }
            }




        }

        
    }
}
