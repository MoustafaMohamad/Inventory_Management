using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Products.DeleteProduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Products.DeleteProduct
{
    [ApiController]
    [Route("api/Products/delete-product")]
    public class DeleteProductEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;
        public DeleteProductEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
           var result = await _mediator.Send(new DeleteProductByIdCommand(id));
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data, result.Message));
        }
    }
}
