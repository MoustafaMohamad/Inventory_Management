using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.StaticsInventory.Number_NoStock.Queries
{
   




    public record Number_NoStockQuery() : IRequest<ResultDto<int>>;
    public class Number_NoStockQueryHandler : BaseRequestHandler<Product, Number_NoStockQuery, ResultDto<int>>
    {
        public Number_NoStockQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {

        }

        public async override Task<ResultDto<int>> Handle(Number_NoStockQuery request, CancellationToken cancellationToken)
        {
            var all_product = await _repository.GetAll();
            if (all_product is null)
            {
                return ResultDto<int>.Faliure(ErrorCode.NoProductsFound, "No product is Found");
            }
            var mappedProducts = all_product.ToList();

            List<Product> products = new List<Product>();
            foreach (var product in mappedProducts)
            {
                if (product.Quantity <= 0)
                {
                    products.Add(product);
                    // message += $"Product={product.Name}     quantity={product.Quantity}   threshold={product.Threshold}  \n";
                }
            }

            var categoryCount = products.Count();

            return ResultDto<int>.Sucess(categoryCount);
        }
    }



}
