
using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;
using MediatR;

namespace Inventory_Management.Features.Products.GetProductDetails.Queries
{
    
    public record GetProductByIdQuery(int id) : IRequest<ResultDto<ProductDto>>;
   
    public class GetProductByIdQueryHandler : BaseRequestHandler<Product, GetProductByIdQuery, ResultDto<ProductDto>> 
    {
        public GetProductByIdQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<ResultDto<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await  _repository.GetByIDWithProjection<ProductDto>(p => p.ID == request.id);//   GetByID(request.id).MapOne<ProductDto>();

            if (product is null)
            {
               throw new BusinessException(ErrorCode.InvalidProductID, "Product is Not Found");
            }
           
            return ResultDto<ProductDto>.Sucess(product);
        }
    }
}
