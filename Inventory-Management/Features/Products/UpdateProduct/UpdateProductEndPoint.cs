using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Products.UpdateProduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Products.UpdateProduct
{
    [ApiController]
    [Route("api/Products/UpdateProduct")]
    public class UpdateProductEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        public UpdateProductEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPut]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProductAsync([FromForm] UpdateProductEndPointRequest request)
        {
            var result = await _mediator.Send(request.MapOne<UpdateProductCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data, result.Message));
        }

    }
}
