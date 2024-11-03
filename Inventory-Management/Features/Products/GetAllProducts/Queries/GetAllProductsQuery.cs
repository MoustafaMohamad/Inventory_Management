using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;
using MediatR;

namespace Inventory_Management.Features.Products.GetAllProducts.Queries
{
    public record GetAllProductsQuery(QueryObject queryParams) : IRequest<ResultDto<IEnumerable<ProductDto>>>;

    public class GetAllProductsQueryHandler : BaseRequestHandler<Product, GetAllProductsQuery, ResultDto<IEnumerable<ProductDto>>> 
    {
        
        public GetAllProductsQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<ResultDto<IEnumerable<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productsQuery = await _repository.GetAll();
            var queryParams = request.queryParams;

            productsQuery = productsQuery
                .Where(p => string.IsNullOrEmpty(queryParams.Name) || p.Name == queryParams.Name)
                .Where(p => request.queryParams.Available == null || p.Available == queryParams.Available);

            if (!productsQuery.Any())
            {
                return ResultDto<IEnumerable<ProductDto>>.Faliure(ErrorCode.NoProductsFound, "No products Found");
            }
            var skipNumber = (request.queryParams.PageNumbar - 1) * request.queryParams.PageSize ;
            productsQuery = productsQuery.Skip(skipNumber).Take(request.queryParams.PageSize);

            var productsDto = productsQuery.Map<ProductDto>().ToList();
            return ResultDto < IEnumerable<ProductDto>>.Sucess(productsDto);
        }
    }
}
