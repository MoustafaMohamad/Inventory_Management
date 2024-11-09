using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.InventoryTransactions.GetAllTransaction.Queries;
using Inventory_Management.Features.Products.GetAllProducts.Queries;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.InventoryTransactions.GetAllTransaction
{
    [ApiController]
    [Route("api/transactions")]
    public class GetAllTransactionsEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public GetAllTransactionsEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTAsync()
        {
            var result = await _mediator.Send(new GetAll_InventoryTransactionQuery());

            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            //var mappedProducts =result.Data.Items.AsQueryable().Map<GetAllProductsEndPointResponse>().ToList();
            //var response = new PaginationResponse<GetAllProductsEndPointResponse>
            //{
            //    TotalNumber = result.Data.TotalNumber,
            //    Items = mappedProducts
            //};

            return Ok(ResultViewModel.Sucess(result.Data, "success"));
        }
    }
}
