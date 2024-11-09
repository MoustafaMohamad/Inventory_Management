using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;
using MediatR;

namespace Inventory_Management.Features.InventoryTransactions.GetAllTransaction.Queries
{

    public record GetAll_InventoryTransactionQuery() : IRequest<ResultDto<InventoryTransaction>>;


    public class GetAll_InventoryTransactionQueryHandler : BaseRequestHandler<InventoryTransaction, GetAll_InventoryTransactionQuery, ResultDto<InventoryTransaction>> 
    {
        
        public GetAll_InventoryTransactionQueryHandler(RequestParameters<InventoryTransaction> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<ResultDto<InventoryTransaction>> Handle(GetAll_InventoryTransactionQuery request, CancellationToken cancellationToken)
        {
            var productsQuery = await _repository.GetAll();
         

            return ResultDto<InventoryTransaction>.Sucess(productsQuery);

        }
    }
}
