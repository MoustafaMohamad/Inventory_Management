using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Products.GetProductDetails.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Products.GetProductDetails
{
    [ApiController]
    [Route("api/Products/GetProductById")]
    public class GetProductByIdEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public GetProductByIdEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            var mappedproduct = result.Data.MapOne<GetProductByIdEndPointResponse>();   
            return Ok(ResultViewModel.Sucess(mappedproduct, result.Message));
        }
    }
}



