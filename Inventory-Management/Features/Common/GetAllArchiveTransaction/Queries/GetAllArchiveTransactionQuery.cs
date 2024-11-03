using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;
using MediatR;

namespace Inventory_Management.Features.Common.GetAllArchiveTransaction.Queries
{
    public record GetAllArchiveTransactionQuery() : IRequest<ResultDto<IEnumerable<InventoryTransaction>>>;

    public class GetAllArchiveTransactionQueryHandler : BaseRequestHandler<InventoryTransaction, GetAllArchiveTransactionQuery, ResultDto<IEnumerable<InventoryTransaction>>> 
    {
        
        public GetAllArchiveTransactionQueryHandler(RequestParameters<InventoryTransaction> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<ResultDto<IEnumerable<InventoryTransaction>>> Handle(GetAllArchiveTransactionQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _repository.GetAll();
            if (transactions.Count()==0)
            {
                throw new BusinessException(ErrorCode.NoProductsFound, "No products Found");
            }
            var transactionsDto = transactions.ToList();
            return ResultDto <IEnumerable<InventoryTransaction>>.Sucess(transactionsDto);
        }
    }
}
