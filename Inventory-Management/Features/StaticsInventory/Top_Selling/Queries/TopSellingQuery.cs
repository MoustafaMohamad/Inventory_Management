using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.StaticsInventory.Top_Selling.Queries
{
    public record TopSellingQuery() : IRequest<ResultDto<Dictionary<int, int>>>;
    public class TopSellingQueryHandler : BaseRequestHandler<InventoryTransaction, TopSellingQuery, ResultDto<Dictionary<int, int>>>
    {
        public TopSellingQueryHandler(RequestParameters<InventoryTransaction> requestParameters) : base(requestParameters)
        {

        }

        public async override Task<ResultDto<Dictionary<int, int>>> Handle(TopSellingQuery request, CancellationToken cancellationToken)
        {
            var inventoryTransactions = await _repository.GetAll();
            if (inventoryTransactions is null)
            {
                return ResultDto<Dictionary<int, int>>.Faliure(ErrorCode.NoTransactionFound, "No Transaction is Found");
            }
            Dictionary<int,int>idsproduct = new Dictionary<int, int>();
            foreach (var transaction in inventoryTransactions)
            {
                if (transaction.TransactionType!= TransactionType.AddStock)
                {
                    if (!idsproduct.ContainsKey(transaction.ProductId))
                    {
                        // Add the key with the calculated value if it does not already exist
                        idsproduct.Add(transaction.ProductId, (int)(transaction.Quantity * transaction.Product.Price));
                    }
                    else
                    {
                        // Update the existing value with the calculated result
                        idsproduct[transaction.ProductId] += (int)(transaction.Quantity * transaction.Product.Price);
                    }


                }

            }



           // var totalRevenue = inventoryTransactions.Sum(a => a.Quantity * a.Product.Price);

            return ResultDto<Dictionary<int, int>>.Sucess(idsproduct);
        }
    }
}
