using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;
using Sprache;

namespace Inventory_Management.Features.StaticsInventory.Number_LowStock.Queries
{
    










    public record Number_LowStockQuery() : IRequest<ResultDto<int>>;
    public class Number_LowStockQueryHandler : BaseRequestHandler<Product, Number_LowStockQuery, ResultDto<int>>
    {
        public Number_LowStockQueryHandler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {

        }

        public async override Task<ResultDto<int>> Handle(Number_LowStockQuery request, CancellationToken cancellationToken)
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
                if (product.Quantity <= product.Threshold)
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
