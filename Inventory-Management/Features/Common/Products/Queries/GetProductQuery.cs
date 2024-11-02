using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.Common.Products.Queries
{
    public record GetProductQuery(int ID) : IRequest<ResultDto<Product>>;


    public class GetProductQueryHandeler : BaseRequestHandler<Product, GetProductQuery, ResultDto<Product>>
    {
        public GetProductQueryHandeler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
           var product = await _repository.FirstOrDefaultAsync( p => p.ID == request.ID);

            if (product is null)
            {
                return ResultDto<Product>.Faliure(ErrorCode.NotFound , "This product is not exists."); 
            }

            return ResultDto<Product>.Sucess(product);
        }


    }
}
