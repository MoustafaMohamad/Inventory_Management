using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.StaticsInventory.Total_Product.Queries
{


    public record Total_ProductQuery() : IRequest<ResultDto<int>>;
    public class Total_ProductQueryHandler : BaseRequestHandler<Product, Total_ProductQuery, ResultDto<int>>
    {
        public Total_ProductQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {

        }

        public async override Task<ResultDto<int>> Handle(Total_ProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAll();
            if (products is null)
            {
                return ResultDto<int>.Faliure(ErrorCode.NoProductsFound, "No product is Found");
            }


            var categoryCount = products.Count();

            return ResultDto<int>.Sucess(categoryCount);
        }
    }

}
