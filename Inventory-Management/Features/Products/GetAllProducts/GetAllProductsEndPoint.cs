using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Products.GetAllProducts.Queries;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Products.GetAllProducts
{
    [ApiController]
    [Route("api/products")]
    public class GetAllProductsEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public GetAllProductsEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] QueryObject queryParams)
        {
            var result = await _mediator.Send(new GetAllProductsQuery(queryParams));

            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            var mappedProducts =result.Data.Items.AsQueryable().Map<GetAllProductsEndPointResponse>().ToList();
            var response = new PaginationResponse<GetAllProductsEndPointResponse>
            {
                TotalNumber = result.Data.TotalNumber,
                Items = mappedProducts
            };

            return Ok(ResultViewModel.Sucess(response, result.Message));
        }
    }
}
