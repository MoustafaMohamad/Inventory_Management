using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.StaticsInventory.Number_Categories.Queries
{
    public record Total_RevenueQuery() : IRequest<ResultDto<int>>;
    public class Total_RevenueQueryHandler : BaseRequestHandler<InventoryTransaction, Total_RevenueQuery, ResultDto<int>>
    {
        public Total_RevenueQueryHandler(RequestParameters<InventoryTransaction> requestParameters) : base(requestParameters)
        {

        }

        public async override Task<ResultDto<int>> Handle(Total_RevenueQuery request, CancellationToken cancellationToken)
        {
            var inventoryTransactions = await _repository.GetAll();
            if (inventoryTransactions is null)
            {
                return ResultDto<int>.Faliure(ErrorCode.NoTransactionFound, "No Transaction is Found");
            }


            var totalRevenue = inventoryTransactions.Sum(a => a.Quantity * a.Product.Price);

            return ResultDto<int>.Sucess((int)totalRevenue);
        }
    }
}
