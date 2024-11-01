using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.GetProductDetails.Queries;
using MediatR;

namespace Inventory_Management.Features.Products.DeleteProduct.Commands
{
    public record DeleteProductByIdCommand(int Id):IRequest<ResultDto<bool>>;
    public class DeleteProductByIdCommandHandler : BaseRequestHandler< Product, DeleteProductByIdCommand, ResultDto<bool>>
    {
        
        public DeleteProductByIdCommandHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
            
        }

        public async override Task<ResultDto<bool>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var existingProductResult = await _mediator.Send(new GetProductByIdQuery(request.Id));
            if (!existingProductResult.IsSuccess)
            {
                return ResultDto<bool>.Faliure(existingProductResult.ErrorCode, existingProductResult.Message);
            }
            var product = existingProductResult.Data.MapOne<Product>();    
            if (product is null)
            {
                return ResultDto<bool>.Faliure(existingProductResult.ErrorCode, existingProductResult.Message);
            }
            _repository.Delete(product);
            //await _repository.SaveChanges();
            return ResultDto<bool>.Sucess(true);
        }
    }

}
