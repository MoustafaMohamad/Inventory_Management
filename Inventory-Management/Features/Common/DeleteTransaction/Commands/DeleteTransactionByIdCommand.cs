using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.GetProductDetails.Queries;
using MediatR;

namespace Inventory_Management.Features.Common.DeleteProduct.Commands
{
    public record DeleteTransactionByIdCommand(int Id):IRequest<ResultDto<bool>>;
    public class DeleteTransactionByIdCommandHandler : BaseRequestHandler< InventoryTransaction, DeleteTransactionByIdCommand, ResultDto<bool>>
    {
        
        public DeleteTransactionByIdCommandHandler(RequestParameters<InventoryTransaction> requestParameters) : base(requestParameters)
        {
            
        }

        public async override Task<ResultDto<bool>> Handle(DeleteTransactionByIdCommand request, CancellationToken cancellationToken)
        {
            var existingTransactionResult =  _repository.FirstAsync(a=>a.ID== request.Id);
            if (existingTransactionResult == null)
            {
                return ResultDto<bool>.Faliure((Inventory_Management.Common.Exceptions.ErrorCode)500, "not found ");
            }
            //var product = existingProductResult.Data.MapOne<InventoryTransaction>();    
            //if (product is null)
            //{
            //    return ResultDto<bool>.Faliure(existingProductResult.ErrorCode, existingProductResult.Message);
            //}
            _repository.Delete(await existingTransactionResult);
            await _repository.SaveChanges();
            return ResultDto<bool>.Sucess(true);
        }
    }

}
