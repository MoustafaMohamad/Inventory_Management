using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Products.Queries;
using MediatR;

namespace Inventory_Management.Features.Common.Products.Commands
{
    public record DecreaseProductQuantityCommand(int ProductId, int Quantity) : IRequest<ResultDto<bool>>;

    public class DecreaseProductQuantityCommandHandeler : BaseRequestHandler<Product, DecreaseProductQuantityCommand, ResultDto<bool>>
    {
        public DecreaseProductQuantityCommandHandeler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<bool>> Handle(DecreaseProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var productresult = await _mediator.Send(new GetProductQuery(request.ProductId));

            if (request.Quantity < 1 && request.Quantity < productresult.Data.Quantity)
            {
                return ResultDto<bool>.Faliure(ErrorCode.BadRequest, "Quantity must be greater than zero and less than product Quantity");
            }

            if (!productresult.IsSuccess)
            {
                return ResultDto<bool>.Faliure(ErrorCode.NotFound, "This product is not exists");
            }



            int updatedQuantity = productresult.Data.Quantity - request.Quantity;
            var updatedProduct = new Product() { ID = request.ProductId, Quantity = updatedQuantity };
            _repository.UpdateIncluded(updatedProduct, nameof(Product.Quantity));

            return ResultDto<bool>.Sucess(updatedProduct);


        }
    }
}
    

