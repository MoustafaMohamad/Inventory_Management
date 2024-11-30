using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Enums;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.AddProduct.Commands;
using Inventory_Management.Features.Products.GetAllProducts.Queries;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;
using Inventory_Management.Features.Products.GetProductDetails.Queries;
using Inventory_Management.Features.Reports.LowStockReport.Dtos;
using Inventory_Management.Features.Reports.LowStockReport.Queries;
using MediatR;

namespace Inventory_Management.Features.Reports.LowStockReport.Commands
{
    public record LowStockReportQuery(int CategoryID) :IRequest<ResultDto<IEnumerable<ProductReportDto>>>;
    public class LowStockReportQueryHandler : BaseRequestHandler<Product, LowStockReportQuery,ResultDto<IEnumerable<ProductReportDto>>>
    {
        public LowStockReportQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
            
        }

        public async override Task<ResultDto<IEnumerable< ProductReportDto>>> Handle(LowStockReportQuery request, CancellationToken cancellationToken)
        {
            var productResult = await _mediator.Send(new GetProductsByStatusQuery(request.CategoryID));
            if (!productResult.IsSuccess) 
            {
                return ResultDto<IEnumerable< ProductReportDto>>.Faliure(productResult.ErrorCode, productResult.Message);
            }
            var product = productResult.Data.MapOne<IEnumerable<ProductReportDto>>();
            return ResultDto<IEnumerable<ProductReportDto> >.Sucess(product);
        }
    }
}
