using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Products.AddProduct;
using Inventory_Management.Features.Products.UpdateProduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Products.UpdateProduct
{
    [ApiController]
    [Route("api/Products/update-product")]
    public class UpdateProductEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UpdateProductValidator _productValidator;
        public UpdateProductEndPoint(IMediator mediator, UpdateProductValidator productValidator)
        {
            _mediator = mediator;
            _productValidator = productValidator;
        }
        [HttpPut("{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProductAsync([FromForm] UpdateProductEndPointRequest request)
        {
            #region Validation
            var validationResults = _productValidator.Validate(request);

            if (!validationResults.IsValid)
            {

                foreach (var error in validationResults.Errors)
                {
                    Console.WriteLine(error.ErrorCode);
                    ErrorCode errorCode = (ErrorCode)Enum.Parse(typeof(ErrorCode), error.ErrorCode);
                    throw new BusinessException(errorCode, error.ErrorMessage);
                }
            }
            #endregion 
            var result = await _mediator.Send(request.MapOne<UpdateProductCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data, result.Message));
        }

    }
}
