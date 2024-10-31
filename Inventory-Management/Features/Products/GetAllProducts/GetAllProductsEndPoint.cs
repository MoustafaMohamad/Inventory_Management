using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Products.GetAllProducts.Queries;
using Inventory_Management.Features.Products.GetProductDetails.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Products.GetAllProducts
{
    [ApiController]
    [Route("api/Products/GetAllProducts")]
    public class GetAllProductsEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public GetAllProductsEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());

            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            var mappedProducts =result.Data.AsQueryable().Map<GetAllProductsEndPointResponse>().ToList();
            return Ok(ResultViewModel.Sucess(mappedProducts, result.Message));
        }
    }
}
