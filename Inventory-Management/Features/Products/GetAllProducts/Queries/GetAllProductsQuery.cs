using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;
using MediatR;

namespace Inventory_Management.Features.Products.GetAllProducts.Queries
{
    public record GetAllProductsQuery() : IRequest<ResultDto<IEnumerable<ProductDto>>>;

    public class GetAllProductsQueryHandler : BaseRequestHandler<Product, GetAllProductsQuery, ResultDto<IEnumerable<ProductDto>>> 
    {
        
        public GetAllProductsQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<ResultDto<IEnumerable<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAll();
            if (products.Count()==0)
            {
                throw new BusinessException(ErrorCode.NoProductsFound, "No products Found");
            }
            var productsDto = products.Map<ProductDto>().ToList();
            return ResultDto < IEnumerable<ProductDto>>.Sucess(productsDto);
        }
    }
}
