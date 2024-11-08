using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using MediatR;
using Inventory_Management.Entities;

namespace Inventory_Management.Features.StaticsInventory.Number_LowStock.Queries
{





    public record Number_CategoriesQuery() : IRequest<ResultDto<int>>;
    public class GetRoleByNameQueryHandler : BaseRequestHandler<Product, Number_CategoriesQuery, ResultDto<int>>
    {
        public GetRoleByNameQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {

        }

        public async override Task<ResultDto<int>> Handle(Number_CategoriesQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAll();
            if (products is null)
            {
                return ResultDto<int>.Faliure(ErrorCode.NoProductsFound, "No product is Found");
            }


            var categoryCount = products
    .Select(p => p.Category)
    .Distinct()
    .Count();

            return ResultDto<int>.Sucess(categoryCount);
        }
    }




}
