using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Products.Queries; 
using MediatR;

namespace Inventory_Management.Features.Common.Products.Commands
{
    public record IncreaseProductQuantityCommand(int ProductId , int Quantity) : IRequest<ResultDto<bool>>;


    public class IncreaseProductQuantityCommandHandler : BaseRequestHandler<Product, IncreaseProductQuantityCommand, ResultDto<bool>>
    {
        public IncreaseProductQuantityCommandHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<bool>> Handle(IncreaseProductQuantityCommand request, CancellationToken cancellationToken)
        {
            if (request.Quantity < 1)
            {
                return ResultDto<bool>.Faliure(ErrorCode.BadRequest, "Quantity must be greater than zero.");
            }
            var result= await _mediator.Send(new GetProductQuery(request.ProductId));

            if (!result.IsSuccess) 
                {
                return ResultDto<bool>.Faliure(ErrorCode.NotFound, "This product is not exists");
                }

            var updatedProduct = result.Data;
            updatedProduct.Quantity += request.Quantity;
           var entity =  _repository.UpdatewithReturn(updatedProduct);
            _repository.SaveChanges();
            //int updatedQuantity = result.Data.Quantity+request.Quantity;
            //var updatedProduct= new Product() { ID =request.ProductId , Quantity = updatedQuantity };

            //_repository.UpdateIncluded(updatedProduct, nameof(Product.Quantity));

            return ResultDto<bool>.Sucess(true);
        }
    }


}
