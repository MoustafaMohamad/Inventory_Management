using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;
using MediatR;
using Inventory_Management.Common.Enums;
using Common.Helpers;

namespace Inventory_Management.Features.Reports.LowStockReport.Queries
{
    public record GetProductsByStatusQuery(int CategoryID) : IRequest<ResultDto<IEnumerable<ProductDto>>>;

    public class GetProductsByStatusQueryHandler : BaseRequestHandler<Product, GetProductsByStatusQuery, ResultDto<IEnumerable<ProductDto>>>
    {

        public GetProductsByStatusQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {

        }

        public async override Task<ResultDto<IEnumerable<ProductDto>>> Handle(GetProductsByStatusQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.Get(p=>p.CategoryID == request.CategoryID && p.Available == productAvailability.LowStock);
            if (products.Count() == 0)
            {
                throw new BusinessException(ErrorCode.NoProductsFound, "No products Found");
            }
            var productsDto = products.Map<ProductDto>().ToList();
            return ResultDto<IEnumerable<ProductDto>>.Sucess(productsDto);
        }
    }
}
