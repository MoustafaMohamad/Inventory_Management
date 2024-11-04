using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;
using MediatR;

namespace Inventory_Management.Features.Products.GetAllProducts.Queries
{
    public record GetAllProductsQuery(QueryObject queryParams) : IRequest<ResultDto<PaginationResponse<ProductDto>>>;

    public class GetAllProductsQueryHandler : BaseRequestHandler<Product, GetAllProductsQuery, ResultDto<PaginationResponse<ProductDto>>> 
    {
        
        public GetAllProductsQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<ResultDto<PaginationResponse<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productsQuery = await _repository.GetAll();
            var queryParams = request.queryParams;
            

            productsQuery = productsQuery
                .Where(p => string.IsNullOrEmpty(queryParams.Name) || p.Name.Contains(queryParams.Name))
                .Where(p => request.queryParams.Available == null || p.Available == queryParams.Available);

            var productCount = productsQuery.Count();

            var pageSize = queryParams.PageSize != 0 ? queryParams.PageSize : 5;
            var pageNumber = queryParams.PageNumbar != 0 ? queryParams.PageNumbar : 1;

            var skipNumber = (pageNumber - 1) * pageSize;
            productsQuery = productsQuery.Skip(skipNumber).Take(pageSize);

            if (!productsQuery.Any())
            {
                return ResultDto< PaginationResponse < ProductDto >>.Faliure(ErrorCode.NoProductsFound, "No products Found");
            }
            var productsDto = productsQuery.Map<ProductDto>().ToList();

            var response = new PaginationResponse<ProductDto>
            {
                TotalNumber = productCount,
                Items = productsDto
            };

            return ResultDto<PaginationResponse<ProductDto>>.Sucess(response);
        }
    }
}
